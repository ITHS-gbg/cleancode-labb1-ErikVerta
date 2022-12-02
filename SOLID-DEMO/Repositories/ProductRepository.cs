using Server.DataAccess;
using Shared;

namespace Server.Repositories
{
    public class ProductRepository<T> : Repository<T> where T : Product
    {
        public ProductRepository(ShopContext shopContext) : base(shopContext)
        {
        }

        public async Task<T> GetAsync(int id)
        {
            return _dbSet.FirstOrDefault(p => p.Id == id);
        }
        public override async Task UpdateAsync(T entity, int id)
        {
            var product = _dbSet.FirstOrDefault(p => p.Id == id);
            product = entity;
        }
    }
}
