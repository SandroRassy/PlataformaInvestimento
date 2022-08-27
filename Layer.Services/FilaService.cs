using Layer.Services.Interfaces;
using Layer.Services.Models.Shared;
using MassTransit;

namespace Layer.Services
{
    public class FilaService : IFilaService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public FilaService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task Publicar(UsuarioPosicaoShared usuarioPosicao)
        {

            _publishEndpoint.Publish<UsuarioPosicaoShared>(usuarioPosicao);

            return Task.CompletedTask;
        }
    }
}
