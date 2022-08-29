using Layer.Domain.Base;
using MongoDB.Bson.Serialization.Attributes;

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
