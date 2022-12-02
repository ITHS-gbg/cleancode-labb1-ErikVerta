using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DataAccess;
using Shared;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController: ControllerBase
    {
        private ShopContext ShopContext { get; set; }

        public OrderController(ShopContext shopContext)
        {
            ShopContext = shopContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await ShopContext.Orders.Include(o => o.Customer).Include(o => o.Products).ToListAsync();
            return Ok(orders);
        }

        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetOrdersForCustomer(int id)
        {
            var orders = await ShopContext.Orders.Include(o => o.Customer).Where(c => c.Customer.Id == id).Include(o => o.Products).ToListAsync();
            if (orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CustomerCart cart)
        {
            var customer = await ShopContext.Customers.FirstOrDefaultAsync(c => c.Id.Equals(cart.CustomerId));
            if (customer is null)
            {
                return BadRequest();
            }

            var products = new List<Product>();

            foreach (var prodId in cart.ProductIds)
            {
                var prod = await ShopContext.Products.FirstOrDefaultAsync(p => p.Id == prodId);
                if (prod is null)
                {
                    return NotFound();
                }
                products.Add(prod);
            }

            var order = new Order() { Customer = customer, Products = products };
            var now = DateTime.Now;
            order.ShippingDate = now.AddDays(5);

            await ShopContext.Orders.AddAsync(order);
            await ShopContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await ShopContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order is null)
            {
                return NotFound();
            }

            ShopContext.Orders.Remove(order);
            await ShopContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("add/{id}")]
        public async Task<IActionResult> AddToOrder(CustomerCart itemsToAdd, int id)
        {
            var customer = await ShopContext.Customers.FirstOrDefaultAsync(c => c.Id.Equals(itemsToAdd.CustomerId));
            if (customer is null)
            {
                return BadRequest();
            }

            var products = new List<Product>();

            foreach (var prodId in itemsToAdd.ProductIds)
            {
                var prod = await ShopContext.Products.FirstOrDefaultAsync(p => p.Id == prodId);
                if (prod is null)
                {
                    return NotFound();
                }
                products.Add(prod);
            }

            var order = await ShopContext.Orders.Include(o => o.Customer).Include(o => o.Products).FirstOrDefaultAsync(o => o.Id == id);
            order.ShippingDate = DateTime.Now.AddDays(5);
            order.Products.AddRange(products);
            await ShopContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("remove/{id}")]
        public async Task<IActionResult> RemoveFromOrder(CustomerCart itemsToRemove, int id)
        {
            var customer = await ShopContext.Customers.FirstOrDefaultAsync(c => c.Id.Equals(itemsToRemove.CustomerId));
            if (customer is null)
            {
                return BadRequest();
            }

            var order = await ShopContext.Orders.Include(o => o.Customer.Id == customer.Id).Include(o => o.Products).FirstOrDefaultAsync(o => o.Id == id);
            order.ShippingDate = DateTime.Now.AddDays(5);

            foreach (var prodId in itemsToRemove.ProductIds)
            {
                var prod = order.Products.FirstOrDefault(p => p.Id == prodId);
                if (prod is null)
                {
                    continue;
                }
                order.Products.Remove(prod);
            }

            await ShopContext.SaveChangesAsync();

            return Ok();
        }
    }
}
