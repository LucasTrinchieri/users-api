namespace Users_api.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task Save();
    }
}
