using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Server.Interfaces;
using Shared;

namespace Server.Services
{
    public class CustomerService : ICustomerService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public CustomerService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await UnitOfWork.CustomerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomer(string email)
        {
            return await UnitOfWork.CustomerRepository.GetAsync(email);
        }

        public async Task CreateCustomer(Customer customer)
        {
            if (!MailAddress.TryCreate(customer.Email, out _))
                throw new ValidationException("Email is not valid");
            await UnitOfWork.CustomerRepository.CreateAsync(customer);
            await UnitOfWork.SaveAsync();
            
        }

        public async Task LoginCustomer(string email, string password)
        {
            var customers = await UnitOfWork.CustomerRepository.GetAllAsync();
            var customer = customers.FirstOrDefault(c => c.Email.Equals(email) && c.Password.Equals(password));
            if (customer is null)
            {
                throw new ValidationException("Email or Password is wrong");
            }
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await UnitOfWork.CustomerRepository.GetAsync(id);

            await UnitOfWork.CustomerRepository.DeleteAsync(customer);
            await UnitOfWork.SaveAsync();
        }
    }
}
