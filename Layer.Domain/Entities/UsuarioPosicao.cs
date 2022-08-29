using Layer.Domain.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace Layer.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public sealed class UsuarioPosicao : Entity
    {
        public string CPF { get; set; }
        public double CheckingAccountAmount { get; set; }
        public double Consolidated { get; set; }
        public List<Posicao> Positions { get; set; }

        public UsuarioPosicao(string cpf, double checkingAccountAmount, double consolidated)
        {
            CPF = cpf;
            CheckingAccountAmount = checkingAccountAmount;
            Consolidated = consolidated;
        }

        public UsuarioPosicao()
        {

        }
    }
}
