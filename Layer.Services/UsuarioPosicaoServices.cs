using Layer.Domain.Entities;
using Layer.Repository.Interfaces;
using Layer.Services.Base;
using Layer.Services.Interfaces;
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
        public UsuarioPosicaoServices(IUsuarioPosicaoRepository usuarioPosicaoRepository) : base(usuarioPosicaoRepository)
        {
            _usuarioPosicaoRepository = usuarioPosicaoRepository;
        }

        public UsuarioPosicao QueryFilter(string cpf)
        {
            var result = _usuarioPosicaoRepository.QueryFilter(cpf);
            return result;
        }
    }
}
