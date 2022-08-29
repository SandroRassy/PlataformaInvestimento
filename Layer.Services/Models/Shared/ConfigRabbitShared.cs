namespace Layer.Services.Models.Shared
{
    public class ConfigRabbitShared
    {
        //public string QueueName { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public string Uri { get; set; }
        public string XQueueMode { get; set; }
    }
}
