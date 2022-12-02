namespace Server.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity, int id);
        public Task DeleteAsync(T entity);
    }
}
