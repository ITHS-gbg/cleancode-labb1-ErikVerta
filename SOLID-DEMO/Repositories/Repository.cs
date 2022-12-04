using Microsoft.EntityFrameworkCore;
using Server.DataAccess;
using Server.Interfaces;
using Shared;

namespace Server.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly ShopContext _shopContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(ShopContext shopContext)
        {
            _shopContext = shopContext;
            _dbSet = _shopContext.Set<T>();
        }

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity, int id)
        {
            var currentEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
            currentEntity = entity;
        }
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

    }
}
