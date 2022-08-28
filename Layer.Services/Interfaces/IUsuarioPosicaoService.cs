using Layer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Services.Interfaces
{
    public interface IUsuarioPosicaoService : IService<UsuarioPosicao>
    {
        UsuarioPosicao QueryFilter(string cpf);
        void Inserir(UsuarioPosicao usuarioposicao);

        void Processar(string payload);
    }
}
