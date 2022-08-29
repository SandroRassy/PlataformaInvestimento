namespace Layer.Services.Interfaces
{
    public interface IService<T>
    {
        IQueryable<T> QueryAll();
        T Query(Guid key);
    }
}
