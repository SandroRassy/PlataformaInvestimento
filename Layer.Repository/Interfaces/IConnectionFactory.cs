using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Repository.Interfaces
{
    public interface IConnectionFactory
    {
        IMongoClient GetClient();

        IMongoDatabase GetDatabase(IMongoClient mongoClient, string databaseName);

        IMongoDatabase GetDatabase(string databaseName);
    }
}
