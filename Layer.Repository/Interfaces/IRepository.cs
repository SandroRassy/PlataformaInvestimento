namespace Layer.Repository.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> QueryAll();
        T Query(Guid key);
        void Insert(T obj);
        void Update(T obj);
    }
}
