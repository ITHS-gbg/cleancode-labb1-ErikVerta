using Microsoft.EntityFrameworkCore;
using Server.DataAccess;
using Shared;

namespace Server.Repositories
{
    public class ProductRepository<T> : Repository<T> where T : Product
    {
        public ProductRepository(ShopContext shopContext) : base(shopContext)
        {
        }

        public override async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return _dbSet.FirstOrDefault(p => p.Id == id);
        }
    }
}
