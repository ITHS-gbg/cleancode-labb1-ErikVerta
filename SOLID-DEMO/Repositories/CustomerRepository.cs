using Microsoft.EntityFrameworkCore;
using Server.DataAccess;
using Shared;

namespace Server.Repositories
{
    public class CustomerRepository<T> : Repository<T> where T : Customer
    {
        public CustomerRepository(ShopContext shopContext) : base(shopContext)
        {
        }
        public override async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(string email)
        {
            var customer = _dbSet.FirstOrDefault(c => c.Email == email);
            if (customer is null)
            {
                throw new NullReferenceException("customer is null");
            }

            return customer;
        }

        public async Task<T> GetAsync(int id)
        {
            var customer = _dbSet.FirstOrDefault(c => c.Id == id);
            if (customer is null)
            {
                throw new NullReferenceException("customer is null");
            }

            return customer;
        }


    }
}
