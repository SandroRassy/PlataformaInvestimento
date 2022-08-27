using Layer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Services.Models.Shared
{
    public class UsuarioPosicaoShared
    {
        public string CPF { get; set; }
        public List<PosicaoShared> Positions { get; set; }

        public UsuarioPosicaoShared(string cpf, List<PosicaoShared> positions)
        {
            CPF = cpf;
            Positions = new List<PosicaoShared>();
            Positions.AddRange(positions);
        }
        public UsuarioPosicaoShared()
        {
            Positions = new List<PosicaoShared>();
        }
    }
}
