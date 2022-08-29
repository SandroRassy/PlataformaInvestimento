using Layer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Repository.Interfaces
{    
    public interface ITendenciaRepository : IRepository<Tendencia>
    {
        Tendencia QueryFilter(string symbol);
    }
}
