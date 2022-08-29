namespace Layer.Domain.Settings
{
    public sealed class MongoDBSetting
    {
        public string DatabaseName { get; set; }
        public List<string> CollectionName { get; set; }
        public string ConnectionString { get; set; }
    }
}
