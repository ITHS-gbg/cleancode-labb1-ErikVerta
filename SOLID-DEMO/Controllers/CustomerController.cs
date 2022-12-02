using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DataAccess;
using Shared;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private ShopContext ShopContext { get; set; }

        public CustomerController(ShopContext shopContext)
        {
            ShopContext = shopContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await ShopContext.Customers.ToListAsync());
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetCustomer(string email)
        {
            return Ok(await ShopContext.Customers.FirstOrDefaultAsync(c => c.Name.Equals(email)));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(Customer customer)
        {
            if (!customer.Name.Contains("@"))
                throw new ValidationException("Email is not an email");
            await ShopContext.AddAsync(customer);
            await ShopContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginCustomer(string email, string password)
        {
            var customer = await ShopContext.Customers.FirstOrDefaultAsync(c => c.Name.Equals(email) && c.Password.Equals(password));
            if (customer is not null)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await ShopContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer is null) return BadRequest();

            ShopContext.Customers.Remove(customer);
            await ShopContext.SaveChangesAsync();
            return Ok();
        }
    }
}
