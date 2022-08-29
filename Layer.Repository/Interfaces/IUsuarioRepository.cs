using Layer.Domain.Entities;

namespace Layer.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario QueryFilter(string cpf);
    }
}
