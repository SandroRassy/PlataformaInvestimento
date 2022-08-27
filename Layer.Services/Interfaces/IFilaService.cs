using Layer.Services.Models.Shared;
using RabbitMQ.Client;
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
        void ConfigFila(string uri, string queuename, bool durable, bool exclusive, bool autoDelete, Dictionary<string, object> _arqs);
    }
}
