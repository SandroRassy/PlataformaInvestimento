using Layer.Domain.Entities;

namespace Layer.Repository.Interfaces
{
    public interface ITendenciaRepository : IRepository<Tendencia>
    {
        Tendencia QueryFilter(string symbol);
    }
}
