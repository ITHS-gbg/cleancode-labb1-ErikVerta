using Microsoft.EntityFrameworkCore;
using Server.DataAccess;
using Server.Interfaces;
using Shared;

namespace Server.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ShopContext _shopContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(ShopContext shopContext)
        {
            _shopContext = shopContext;
            _dbSet = _shopContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public abstract Task UpdateAsync(T entity, int id);
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

    }
}
