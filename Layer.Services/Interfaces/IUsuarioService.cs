using Layer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Services.Interfaces
{
    public interface IUsuarioService : IService<Usuario>
    {
        Usuario QueryFilter(string cpf);

        void Inserir(string nome, string codigoconta, string cpf);
    }
}
