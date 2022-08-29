using Layer.Services.Models.Shared;
using RabbitMQ.Client;

namespace Layer.Services.Interfaces
{
    public interface IFilaService
    {
        Task Publicar(UsuarioPosicaoShared usuarioPosicao);
        void ConfigFila(string uri, string queuename, bool durable, bool exclusive, bool autoDelete, Dictionary<string, object> _arqs);
        IModel? Canal();
    }
}
