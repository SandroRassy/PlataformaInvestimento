using Layer.Domain.Base;
using MongoDB.Bson.Serialization.Attributes;

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

        public Tendencia()
        {

        }
    }
}
