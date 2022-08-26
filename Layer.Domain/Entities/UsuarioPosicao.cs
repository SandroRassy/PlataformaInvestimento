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
