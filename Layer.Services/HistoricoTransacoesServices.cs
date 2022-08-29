using Layer.Domain.Entities;
using Layer.Repository.Interfaces;
using Layer.Services.Base;
using Layer.Services.Interfaces;

namespace Layer.Services
{
    public class HistoricoTransacoesServices : Services<HistoricoTransacoes>, IHistoricoTransacoesService
    {
        private readonly IHistoricoTransacoesRepository _historicoTransacoesRepository;
        public HistoricoTransacoesServices(IHistoricoTransacoesRepository historicoTransacoesRepository) : base(historicoTransacoesRepository)
        {
            _historicoTransacoesRepository = historicoTransacoesRepository;
        }

        public void Inserir(HistoricoTransacoes historicotransacoes)
        {
            _historicoTransacoesRepository.Insert(historicotransacoes);
        }
    }
}
