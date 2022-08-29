using Layer.Domain.Entities;

namespace Layer.Services.Interfaces
{
    public interface ITendenciaService : IService<Tendencia>
    {
        void Inserir(string symbol, double currentprice);
        Tendencia QueryFilter(string symbol);
    }
}
