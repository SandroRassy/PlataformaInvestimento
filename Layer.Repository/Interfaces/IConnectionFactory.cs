using MongoDB.Driver;

namespace Layer.Repository.Interfaces
{
    public interface IConnectionFactory
    {
        IMongoClient GetClient();

        IMongoDatabase GetDatabase(IMongoClient mongoClient, string databaseName);

        IMongoDatabase GetDatabase(string databaseName);
    }
}
