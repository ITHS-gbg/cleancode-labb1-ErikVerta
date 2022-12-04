using Microsoft.EntityFrameworkCore;
using Server.DataAccess;
using Server.Repositories;
using Shared;

namespace Server.Interfaces
{
    public interface IUnitOfWork
    {
        public CustomerRepository<Customer> CustomerRepository { get; }
        public OrderRepository<Order> OrderRepository { get; }
        public ProductRepository<Product> ProductRepository { get; }
        public Task SaveAsync();
    }
}
