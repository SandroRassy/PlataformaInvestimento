using Layer.Domain.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace Layer.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public sealed class Usuario : Entity
    {
        public string Nome { get; set; }
        public string CodigoConta { get; set; }
        public string CPF { get; set; }

        public Usuario(string cpf, string nome, string codigoconta)
        {
            Nome = nome;
            CodigoConta = codigoconta;
            CPF = cpf;
        }

        public Usuario()
        {

        }

    }
}
