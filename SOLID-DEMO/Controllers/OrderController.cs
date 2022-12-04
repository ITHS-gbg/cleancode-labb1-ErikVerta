using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Shared;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController: ControllerBase
    {
        private IOrderService OrderService { get; set; }

        public OrderController(IOrderService orderService)
        {
            OrderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await OrderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetOrdersForCustomer(int id)
        {
            var orders = await OrderService.GetOrdersForCustomer(id);
            if (!orders.Any())
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CustomerCart cart)
        {
            try
            {
                await OrderService.CreateOrder(cart);
            }
            catch (NullReferenceException exception)
            {
                return NotFound(exception.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            try
            {
                await OrderService.DeleteOrder(id);
            }
            catch (NullReferenceException exception)
            {
                return NotFound(exception.Message);
            }
            return Ok();
        }

        [HttpPatch("add/{id}")]
        public async Task<IActionResult> AddToOrder(CustomerCart itemsToAdd, int id)
        {
            try
            {
                await OrderService.AddToOrder(itemsToAdd, id);
            }
            catch (NullReferenceException exception)
            {
                return NotFound(exception.Message);
            }

            return Ok();
        }

        [HttpPatch("remove/{id}")]
        public async Task<IActionResult> RemoveFromOrder(CustomerCart itemsToRemove, int id)
        {
            try
            {
                await OrderService.RemoveFromOrder(itemsToRemove, id);
            }
            catch (NullReferenceException exception)
            {
                return NotFound(exception.Message);
            }

            return Ok();
        }
    }
}
