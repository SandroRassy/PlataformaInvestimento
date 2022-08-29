using Layer.Domain.Entities;

namespace Layer.Services.Interfaces
{
    public interface IHistoricoTransacoesService : IService<HistoricoTransacoes>
    {
        void Inserir(HistoricoTransacoes historicotransacoes);
    }
}
