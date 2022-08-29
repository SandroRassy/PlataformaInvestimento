using Layer.Domain.Entities;

namespace Layer.Services.Interfaces
{
    public interface IUsuarioService : IService<Usuario>
    {
        Usuario QueryFilter(string cpf);

        void Inserir(string nome, string codigoconta, string cpf);
    }
}
