using Layer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Services.Interfaces
{
    public interface ITendenciaService : IService<Tendencia>
    {
        void Inserir(string symbol, double currentprice);
        Tendencia QueryFilter(string symbol);
    }
}
