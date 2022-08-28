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
    public sealed class HistoricoTransacoes : Entity
    {
        public string CPF { get; set; }
        public string Payload { get; set; }
        public int Status { get; set; }
        public string Obs { get; set; }
        public DateTime DataTransacao { get; set; }
    }
}
