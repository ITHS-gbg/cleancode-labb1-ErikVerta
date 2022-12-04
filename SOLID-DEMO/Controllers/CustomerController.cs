using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Shared;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerService CustomerService { get; set; }

        public CustomerController(ICustomerService customerService)
        {
            CustomerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await CustomerService.GetCustomers());
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetCustomer(string email)
        {
            try
            {
                var customer = await CustomerService.GetCustomer(email);
                return Ok(customer);
            }
            catch (NullReferenceException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(Customer customer)
        {
            try
            {
                await CustomerService.CreateCustomer(customer);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginCustomer(string email, string password)
        {
            try
            {
                await CustomerService.LoginCustomer(email, password);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await CustomerService.DeleteCustomer(id);
            }
            catch (NullReferenceException exception)
            {
                return NotFound(exception.Message);
            }
            return Ok();
        }
    }
}
