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
    public sealed class Posicao : Entity
    {
        public string Symbol { get; set; }
        public string Amount { get; set; }
        public double CurrentPrice { get; set; }

        public Posicao(string symbol, string amount, double currentPrice)
        {
            Symbol = symbol;
            Amount = amount;
            CurrentPrice = currentPrice;    
        }
    }
}
