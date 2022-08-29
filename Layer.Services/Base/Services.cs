using Layer.Repository.Interfaces;
using Layer.Services.Interfaces;

namespace Layer.Services.Base
{
    public abstract class Services<T> : IService<T>
    {
        private readonly IRepository<T> _repository;
        public Services(IRepository<T> repository)
        {
            _repository = repository;
        }

        protected void Insert(T obj)
        {
            _repository.Insert(obj);
        }

        public T Query(Guid key)
        {
            return _repository.Query(key);
        }

        public IQueryable<T> QueryAll()
        {
            return _repository.QueryAll();
        }
    }
}
