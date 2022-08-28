using Layer.Domain.Entities;
using Layer.Repository.Base;
using Layer.Repository.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
