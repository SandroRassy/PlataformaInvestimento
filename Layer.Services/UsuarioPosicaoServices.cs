﻿using Layer.Domain.Entities;
using Layer.Repository.Interfaces;
using Layer.Services.Base;
using Layer.Services.Interfaces;
using Layer.Services.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Services
{    
    public class UsuarioPosicaoServices : Services<UsuarioPosicao>, IUsuarioPosicaoService
    {
        private readonly IUsuarioPosicaoRepository _usuarioPosicaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IFilaService _filaService;
        public UsuarioPosicaoServices(IUsuarioPosicaoRepository usuarioPosicaoRepository, IUsuarioRepository  usuarioRepository, IFilaService filaService) : base(usuarioPosicaoRepository)
        {
            _usuarioPosicaoRepository = usuarioPosicaoRepository;
            _usuarioRepository = usuarioRepository;
            _filaService = filaService;
        }

        public void Inserir(UsuarioPosicao usuarioposicao)
        {
            var usuario = _usuarioRepository.QueryFilter(usuarioposicao.CPF);

            if(usuario != null) 
            {
                var posicao = _usuarioPosicaoRepository.QueryFilter(usuarioposicao.CPF);

                if (posicao != null)
                {
                    if(usuarioposicao.Positions.Count == 0)
                        throw new Exception($"Lista de compra não pode estar vazia.");

                    if(posicao.CheckingAccountAmount == 0)
                        throw new Exception($"Usuário não tem saldo suficiente.");

                    //validar se o cliente tem saldo para comprar as ações
                    if (VerificarSaldoConta(usuarioposicao, posicao.CheckingAccountAmount))
                    {
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

            if (saldo > totalCompra)
                return true;
            else
                return false;
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
    }
}
