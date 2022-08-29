using Layer.Domain.Interfaces;
using Layer.Repository.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Layer.Repository.Base
{
    public abstract class Repository<T> : IRepository<T> where T : IEntity
    {
        public readonly IMongoCollection<T> _collectionName;

        protected Repository(IMongoCollection<T> collectionName)
        {
            _collectionName = collectionName;
        }

        protected Repository(IConnectionFactory connectionFactory, string databaseName, string collectionName)
        {
            _collectionName = connectionFactory.GetDatabase(databaseName).GetCollection<T>(collectionName);

        }

        public void Insert(T obj)
        {
            _collectionName.InsertOne(obj);
        }

        public T Query(Guid key)
        {
            return _collectionName.AsQueryable<T>().FirstOrDefault(w => w.Key == key);
        }

        public IQueryable<T> QueryAll()
        {
            return _collectionName.AsQueryable<T>();
        }

        public void Update(T obj)
        {
            Expression<Func<T, bool>> filter = x => x.Key.Equals(obj.Key);
            _collectionName.ReplaceOne(filter, obj);
        }
    }
}
