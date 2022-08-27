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
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel? _channel;

        private string _uri;
        private string _queueName;
        private bool _durable;
        private bool _exclusive;
        private bool _autoDelete;
        private Dictionary<string, object> arqs;
        

        public FilaService()
        {
            
        }

        public void ConfigFila(string uri, string queuename, bool durable, bool exclusive, bool autoDelete, Dictionary<string, object> _arqs)
        {
            _uri = uri;
            _queueName = queuename;
            _durable = durable;
            _exclusive = exclusive;
            _autoDelete = autoDelete;            
            arqs = _arqs;
        }

        private void Conectar(string uri)
        {
            _factory =  new ConnectionFactory { Uri = new Uri(uri) };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task Publicar(UsuarioPosicaoShared usuarioPosicao)
        {
            Conectar(_uri);                                  
            _channel.QueueDeclare(_queueName, _durable, _exclusive, _autoDelete, arqs);

            string message = JsonSerializer.Serialize(usuarioPosicao);            
            var data = Encoding.UTF8.GetBytes(message);            
            var exchangeName = "";
            var routingKey = _queueName;
            _channel.BasicPublish(exchangeName, routingKey, null, data);

            return Task.CompletedTask;
        }        
    }
}
