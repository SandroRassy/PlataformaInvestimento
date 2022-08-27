using Layer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;
using Layer.Services.Interfaces;
using Layer.Services;

namespace Layer.Workers
{
    public class ConsoleApp
    {
        private readonly IUsuarioPosicaoService _usuarioPosicaoService;
        private readonly IConfigurationRoot _configuration;
        private readonly IFilaService _filaService;
        public ConsoleApp(IUsuarioPosicaoService usuarioPosicaoService, IConfigurationRoot configuration, IFilaService filaService)
        {
            _usuarioPosicaoService = usuarioPosicaoService;
            _configuration = configuration;
            _filaService = filaService;
        }

        public void Run()
        {
            var queueName = _configuration["UsuarioPosicaoFila:queueName"];

            IniciarFila(queueName);

            using var channel = _filaService.Canal();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _usuarioPosicaoService.Processar(message);

                Console.WriteLine($"Message received: {message}");
                //Console.WriteLine($"CPF: {teste.CPF}");
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            Console.ReadKey();

        }

        private void IniciarFila(string queuename)
        {            
            var durable = bool.Parse(_configuration["UsuarioPosicaoFila:durable"]);
            var exclusive = bool.Parse(_configuration["UsuarioPosicaoFila:exclusive"]);
            var autoDelete = bool.Parse(_configuration["UsuarioPosicaoFila:autoDelete"]);
            var uri = _configuration["RabbitMq:uri"];

            Dictionary<string, object> args = new Dictionary<string, object>()
            {
                { "x-queue-mode", _configuration["UsuarioPosicaoFila:x-queue-mode"] }
            };

            _filaService.ConfigFila(uri, queuename, durable, exclusive, autoDelete, args);            
        }
    }
}
