using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DataAccess;
using Server.Interfaces;
using Shared;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private IUnitOfWork UnitOfWork { get; }

        public CustomerController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await UnitOfWork.CustomerRepository.GetAllAsync());
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCustomer(string name)
        {
            return Ok(await UnitOfWork.CustomerRepository.GetAsync(name));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(Customer customer)
        {
            if (!customer.Name.Contains("@"))
                throw new ValidationException("Email is not an email");
            await UnitOfWork.CustomerRepository.CreateAsync(customer);
            await UnitOfWork.SaveAsync();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginCustomer(string email, string password)
        {
            var customers = await UnitOfWork.CustomerRepository.GetAllAsync();
            var customer = customers.FirstOrDefault(c => c.Name.Equals(email) && c.Password.Equals(password));
            if (customer is not null)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customers = await UnitOfWork.CustomerRepository.GetAllAsync();
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer is null) return BadRequest();

            await UnitOfWork.CustomerRepository.DeleteAsync(customer);
            await UnitOfWork.SaveAsync();
            return Ok();
        }
    }
}
