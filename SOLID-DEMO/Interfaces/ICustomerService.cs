using Shared;

namespace Server.Interfaces;

public interface ICustomerService : IService
{
    Task<IEnumerable<Customer>> GetCustomers();
    Task<Customer> GetCustomer(string email);
    Task CreateCustomer(Customer customer);
    Task LoginCustomer(string email, string password);
    Task DeleteCustomer(int id);
}