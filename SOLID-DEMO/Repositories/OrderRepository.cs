using Microsoft.EntityFrameworkCore;
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
            var order = await _dbSet
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order is null)
            {
                throw new NullReferenceException("order is null");
            }
            return order;
        }

        public override async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.Include(o => o.Customer).Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();
        }
    }
}
