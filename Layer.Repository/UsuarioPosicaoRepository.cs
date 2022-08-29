using Layer.Domain.Entities;
using Layer.Repository.Base;
using Layer.Repository.Interfaces;
using MongoDB.Driver;

namespace Layer.Repository
{
    public sealed class UsuarioPosicaoRepository : Repository<UsuarioPosicao>, IUsuarioPosicaoRepository
    {
        public UsuarioPosicaoRepository(IMongoCollection<UsuarioPosicao> collectionName) : base(collectionName)
        {
        }

        public UsuarioPosicaoRepository(IConnectionFactory connectionFactory, string databaseName, string collectionName)
            : base(connectionFactory, databaseName, collectionName)
        {
        }

        public UsuarioPosicao QueryFilter(string cpf)
        {
            var retorno = new UsuarioPosicao();

            if (!String.IsNullOrEmpty(cpf))
                retorno = _collectionName.AsQueryable<UsuarioPosicao>().FirstOrDefault(w => w.CPF == cpf);

            return retorno;
        }
    }
}
