using Layer.Services.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Services.Interfaces
{
    public interface IFilaService
    {
        Task Publicar(UsuarioPosicaoShared usuarioPosicao);
    }
}
