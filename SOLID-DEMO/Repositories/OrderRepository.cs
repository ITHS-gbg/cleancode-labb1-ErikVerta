using Server.DataAccess;
using Shared;

namespace Server.Repositories
{
    public class OrderRepository<T> : Repository<T> where T : Order
    {
        public OrderRepository(ShopContext shopContext) : base(shopContext)
        {
        }

        public async Task<T> GetAsync(int id)
        {
            return _dbSet.FirstOrDefault(o => o.Id == id);
        }

        public override async Task UpdateAsync(T entity, int id)
        {
            var order = _dbSet.FirstOrDefault(o => o.Id == id);
            order = entity;
        }
    }
}
