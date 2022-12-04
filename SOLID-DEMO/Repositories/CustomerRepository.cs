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
            return _dbSet.FirstOrDefault(c => c.Email == email);
        }

        public async Task<T> GetAsync(int id)
        {
            return _dbSet.FirstOrDefault(c => c.Id == id);
        }


    }
}
