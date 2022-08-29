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
    public sealed class TendenciaRepository : Repository<Tendencia>, ITendenciaRepository
    {
        public TendenciaRepository(IMongoCollection<Tendencia> collectionName) : base(collectionName)
        {
        }

        public TendenciaRepository(IConnectionFactory connectionFactory, string databaseName, string collectionName)
            : base(connectionFactory, databaseName, collectionName)
        {
        }

        public Tendencia QueryFilter(string symbol)
        {
            var retorno = new Tendencia();

            if (!String.IsNullOrEmpty(symbol))
                retorno = _collectionName.AsQueryable<Tendencia>().FirstOrDefault(w => w.Symbol == symbol);

            return retorno;
        }
    }
}
