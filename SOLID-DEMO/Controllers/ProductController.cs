using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DataAccess;
using Shared;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private ShopContext ShopContext { get; set; }

        public ProductController(ShopContext shopContext)
        {
            ShopContext = shopContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await ShopContext.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return Ok(await ShopContext.Products.FirstOrDefaultAsync(c => c.Name.Equals(id)));
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product newProd)
        {
            var prod = await ShopContext.Products.FirstOrDefaultAsync(p => p.Name.Equals(newProd.Name));
            if (prod == null)
            {
                await ShopContext.Products.AddAsync(newProd);
                await ShopContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }
    }
}
