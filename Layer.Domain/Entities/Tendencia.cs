using Layer.Domain.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public sealed class Tendencia : Entity
    {
        public string Symbol { get; set; }        
        public double CurrentPrice { get; set; }

        public Tendencia(string symbol, double currentPrice)
        {
            Symbol = symbol;
            CurrentPrice = currentPrice;
        }
    }
}
