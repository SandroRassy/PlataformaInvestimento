using Layer.Services.Interfaces;
using Layer.Services.Models.Shared;
using System.Text.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Layer.Services
{
    public class FilaService : IFilaService
    {
        
        public FilaService()
        {
            
        }

        public Task Publicar(UsuarioPosicaoShared usuarioPosicao)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqps://zpnseqco:7cUFexcS37KfRZ0w0Q2F7PZYYJ-nWJMs@beaver.rmq.cloudamqp.com/zpnseqco")
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var queueName = "UsuarioPosicao";
            bool durable = true;
            bool exclusive = false;
            bool autoDelete = false;

            Dictionary<string, object> args = new Dictionary<string, object>()
            {
                { "x-queue-mode", "lazy" }
            };

            channel.QueueDeclare(queueName, durable, exclusive, autoDelete, args);

            string message = JsonSerializer.Serialize(usuarioPosicao);            
            var data = Encoding.UTF8.GetBytes(message);
            // publish to the "default exchange", with the queue name as the routing key
            var exchangeName = "";
            var routingKey = queueName;
            channel.BasicPublish(exchangeName, routingKey, null, data);

            return Task.CompletedTask;
        }
    }
}
