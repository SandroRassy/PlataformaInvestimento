using Layer.Domain.Entities;
using Layer.Repository;
using Layer.Repository.Interfaces;
using Layer.Services.Base;
using Layer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentValidator;

namespace Layer.Services
{    
    public class UsuarioServices : Services<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioServices(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void Inserir(string nome, string codigoconta, string cpf)
        {
            var obj = new Usuario(cpf, nome, codigoconta);

            if (String.IsNullOrEmpty(obj.CPF))
                throw new Exception($"o campo CPF não pode ser vazio.");

            if (String.IsNullOrEmpty(obj.Nome))
                throw new Exception($"o campo Nome não pode ser vazio.");

            if (String.IsNullOrEmpty(obj.CodigoConta))
                throw new Exception($"o campo CodigoConta não pode ser vazio.");

            if (!CpfValidation.Validate(obj.CPF))
                throw new Exception($"CPF Inválido!");

            _usuarioRepository.Insert(obj);
        }

        public Usuario QueryFilter(string cpf)
        {
            var result = _usuarioRepository.QueryFilter(cpf);
            return result;
        }
    }
}
