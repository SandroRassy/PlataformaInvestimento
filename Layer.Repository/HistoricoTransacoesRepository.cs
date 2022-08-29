using Layer.Domain.Entities;
using Layer.Repository.Base;
using Layer.Repository.Interfaces;
using MongoDB.Driver;

namespace Layer.Repository
{
    public sealed class HistoricoTransacoesRepository : Repository<HistoricoTransacoes>, IHistoricoTransacoesRepository
    {
        public HistoricoTransacoesRepository(IMongoCollection<HistoricoTransacoes> collectionName) : base(collectionName)
        {
        }

        public HistoricoTransacoesRepository(IConnectionFactory connectionFactory, string databaseName, string collectionName)
            : base(connectionFactory, databaseName, collectionName)
        {
        }


    }
}
