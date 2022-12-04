using Server.Interfaces;
using Server.Repositories;
using Shared;

namespace Server.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShopContext _shopContext;
        public CustomerRepository<Customer> CustomerRepository { get; }
        public OrderRepository<Order> OrderRepository { get; }
        public ProductRepository<Product> ProductRepository { get; }

        public UnitOfWork(ShopContext shopContext)
        {
            _shopContext = shopContext;
            CustomerRepository = new CustomerRepository<Customer>(_shopContext);
            OrderRepository = new OrderRepository<Order>(_shopContext);
            ProductRepository = new ProductRepository<Product>(_shopContext);
        }

        public async Task SaveAsync()
        {
            await _shopContext.SaveChangesAsync();
        }
    }
}
