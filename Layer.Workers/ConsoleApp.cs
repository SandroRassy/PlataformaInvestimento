using Layer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Layer.Workers
{
    public class ConsoleApp
    {
        private readonly IUsuarioPosicaoRepository _usuarioPosicaoRepository;
        public ConsoleApp(IUsuarioPosicaoRepository usuarioPosicaoRepository)
        {
            _usuarioPosicaoRepository = usuarioPosicaoRepository;
        }

        public void Run()
        {
            var teste = _usuarioPosicaoRepository.QueryFilter("08309184778");
            
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqps://zpnseqco:7cUFexcS37KfRZ0w0Q2F7PZYYJ-nWJMs@beaver.rmq.cloudamqp.com/zpnseqco")
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            bool durable = true;
            bool exclusive = false;
            bool autoDelete = false;

            Dictionary<string, object> args = new Dictionary<string, object>()
            {
                { "x-queue-mode", "lazy" }
            };

            channel.QueueDeclare("UsuarioPosicaoShared", durable, exclusive, autoDelete, args);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message received: {message}");
            };

            channel.BasicConsume(queue: "UsuarioPosicaoShared", autoAck: true, consumer: consumer);

            Console.ReadKey();

        }
    }
}
