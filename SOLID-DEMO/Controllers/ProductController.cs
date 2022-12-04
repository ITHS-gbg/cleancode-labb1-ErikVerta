﻿using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Shared;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private IProductService ProductService { get; set; } 

        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await ProductService.GetProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await ProductService.GetProduct(id);
            if (product is null)
            {
                return NotFound("product does not exist");
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product newProduct)
        {
            try
            {
                await ProductService.CreateProduct(newProduct);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok();
        }
    }
}
