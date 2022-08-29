using Layer.Services.Interfaces;

namespace Layer.Services
{
    public class ConfigRabbitUsuarioPosicao : IConfigRabbit
    {
        public string QueueName { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public string Uri { get; set; }
        public string XQueueMode { get; set; }

        public ConfigRabbitUsuarioPosicao(string queuename, bool durable, bool exclusive, bool autoDelete, string uri, string xqueuemode)
        {
            QueueName = queuename;
            Durable = durable;
            Exclusive = exclusive;
            AutoDelete = autoDelete;
            Uri = uri;
            XQueueMode = xqueuemode;
        }
    }
}
