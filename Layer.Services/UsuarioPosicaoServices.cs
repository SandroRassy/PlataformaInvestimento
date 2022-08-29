using Layer.Domain.Entities;
using Layer.Repository.Interfaces;
using Layer.Services.Base;
using Layer.Services.Interfaces;
using Layer.Services.Models.Shared;
using System.Text.Json;


namespace Layer.Services
{
    public class UsuarioPosicaoServices : Services<UsuarioPosicao>, IUsuarioPosicaoService
    {
        private readonly IUsuarioPosicaoRepository _usuarioPosicaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IHistoricoTransacoesService _historicoTransacoesService;
        private readonly IFilaService _filaService;
        private readonly IConfigRabbit _configRabbit;
        private double _totalCompra = 0;
        public UsuarioPosicaoServices(IUsuarioPosicaoRepository usuarioPosicaoRepository, IUsuarioRepository usuarioRepository, IFilaService filaService, IConfigRabbit _configrabbit, IHistoricoTransacoesService historicoTransacoesService) : base(usuarioPosicaoRepository)
        {
            _usuarioPosicaoRepository = usuarioPosicaoRepository;
            _usuarioRepository = usuarioRepository;
            _historicoTransacoesService = historicoTransacoesService;
            _filaService = filaService;
            _configRabbit = _configrabbit;
        }

        public void Inserir(UsuarioPosicao usuarioposicao)
        {
            var usuario = _usuarioRepository.QueryFilter(usuarioposicao.CPF);

            if (usuario != null)
            {
                var posicao = _usuarioPosicaoRepository.QueryFilter(usuarioposicao.CPF);

                if (posicao != null)
                {
                    if (usuarioposicao.Positions.Count == 0)
                        throw new Exception($"Lista de compra não pode estar vazia.");

                    if (posicao.CheckingAccountAmount == 0)
                        throw new Exception($"Usuário não tem saldo suficiente.");

                    //validar se o cliente tem saldo para comprar as ações
                    if (VerificarSaldoConta(usuarioposicao, posicao.CheckingAccountAmount))
                    {
                        Dictionary<string, object> args = new Dictionary<string, object>()
                        {
                            { "x-queue-mode", _configRabbit.XQueueMode }
                        };

                        _filaService.ConfigFila(_configRabbit.Uri, _configRabbit.QueueName, _configRabbit.Durable, _configRabbit.Exclusive, _configRabbit.AutoDelete, args);
                        _filaService.Publicar(FillPayload(usuarioposicao));


                    }
                    else
                        throw new Exception($"Usuário não tem saldo suficiente.");
                }
                else
                {
                    throw new Exception($"Erro na conta do usuário.");
                }
            }
            else
            {
                throw new Exception($"Usuário não cadastrado.");
            }
        }

        public UsuarioPosicao QueryFilter(string cpf)
        {
            var result = _usuarioPosicaoRepository.QueryFilter(cpf);
            return result;
        }

        private bool VerificarSaldoConta(UsuarioPosicao usuarioposicao, double saldo)
        {
            double totalCompra = 0;
            foreach (var posicao in usuarioposicao.Positions)
            {
                totalCompra = +posicao.CurrentPrice;
            }

            _totalCompra = totalCompra;

            if (saldo > totalCompra)
                return true;
            else
                return false;
        }

        private double TotalValorPosicoes(UsuarioPosicao usuarioposicao)
        {
            double valor = 0;
            foreach (var posicao in usuarioposicao.Positions)
            {
                valor = +(posicao.CurrentPrice * double.Parse(posicao.Amount));
            }
            return valor;
        }

        private UsuarioPosicaoShared FillPayload(UsuarioPosicao usuarioposicao)
        {
            var retorno = new UsuarioPosicaoShared();
            retorno.CPF = usuarioposicao.CPF;

            foreach (var posicao in usuarioposicao.Positions)
            {
                var novaposicao = new PosicaoShared(posicao.Symbol, posicao.Amount, posicao.CurrentPrice);
                retorno.Positions.Add(novaposicao);
            }

            return retorno;
        }

        private UsuarioPosicao FillPayloadShared(UsuarioPosicaoShared usuarioposicao)
        {
            var retorno = new UsuarioPosicao();
            retorno.CPF = usuarioposicao.CPF;
            retorno.Positions = new List<Posicao>();

            foreach (var posicao in usuarioposicao.Positions)
            {
                var novaposicao = new Posicao(posicao.Symbol, posicao.Amount, posicao.CurrentPrice);
                retorno.Positions.Add(novaposicao);
            }

            return retorno;
        }

        public void Processar(string payload)
        {
            var _payload = JsonSerializer.Deserialize<UsuarioPosicaoShared>(payload);

            if (_payload != null)
            {
                var historicotransacoes = new HistoricoTransacoes();
                historicotransacoes.CPF = _payload.CPF;
                historicotransacoes.DataTransacao = DateTime.Now;
                historicotransacoes.Payload = payload;

                var posicaoAtual = _usuarioPosicaoRepository.QueryFilter(_payload.CPF);

                if (posicaoAtual.CheckingAccountAmount > 0)
                {
                    var posicaoNova = FillPayloadShared(_payload);

                    if (VerificarSaldoConta(posicaoNova, posicaoAtual.CheckingAccountAmount))
                    {
                        //debitar o valor da compra
                        posicaoAtual.CheckingAccountAmount = posicaoAtual.CheckingAccountAmount - _totalCompra;

                        if (posicaoAtual.Positions == null)
                            posicaoAtual.Positions = new List<Posicao>();

                        posicaoAtual.Positions.AddRange(posicaoNova.Positions);

                        //calcular  Consolidated                      
                        posicaoAtual.Consolidated = posicaoAtual.CheckingAccountAmount + TotalValorPosicoes(posicaoAtual);

                        _usuarioPosicaoRepository.Update(posicaoAtual);

                        historicotransacoes.Status = 1;
                    }
                    else
                    {
                        historicotransacoes.Obs = $"O cliente não tem saldo para efetuar a compra";
                        historicotransacoes.Status = 2;
                    }
                }
                else
                {
                    historicotransacoes.Obs = $"O Saldo do cliente não pode ser vazio";
                    historicotransacoes.Status = 2;
                }
                _historicoTransacoesService.Inserir(historicotransacoes);
            }
        }
    }
}
