using Layer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Services.Interfaces
{
    public interface IHistoricoTransacoesService : IService<HistoricoTransacoes>
    {
        void Inserir(HistoricoTransacoes historicotransacoes);
    }
}
