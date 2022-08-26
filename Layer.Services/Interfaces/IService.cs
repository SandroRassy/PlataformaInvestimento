using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Services.Interfaces
{
    public interface IService<T>
    {
        IQueryable<T> QueryAll();
        T Query(Guid key);
        void Insert(T obj);
    }
}
