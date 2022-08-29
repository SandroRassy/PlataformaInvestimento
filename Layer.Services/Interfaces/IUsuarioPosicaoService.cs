using Layer.Domain.Entities;

namespace Layer.Services.Interfaces
{
    public interface IUsuarioPosicaoService : IService<UsuarioPosicao>
    {
        UsuarioPosicao QueryFilter(string cpf);
        void Inserir(UsuarioPosicao usuarioposicao);

        void Processar(string payload);
    }
}
