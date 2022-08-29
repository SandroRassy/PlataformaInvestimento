using Layer.Domain.Entities;

namespace Layer.Repository.Interfaces
{
    public interface IUsuarioPosicaoRepository : IRepository<UsuarioPosicao>
    {
        UsuarioPosicao QueryFilter(string cpf);
    }
}
