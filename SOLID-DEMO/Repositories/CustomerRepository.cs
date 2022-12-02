using Server.DataAccess;
using Shared;

namespace Server.Repositories
{
    public class CustomerRepository<T> : Repository<T> where T : Customer
    {
        public CustomerRepository(ShopContext shopContext) : base(shopContext)
        {
        }

        public async Task<T> GetAsync(string name)
        {
            return _dbSet.FirstOrDefault(c => c.Name == name);
        }

        public override async Task UpdateAsync(T entity, int id)
        {
            var customer = _dbSet.FirstOrDefault(c => c.Id == id);
            customer = entity;
        }
    }
}
