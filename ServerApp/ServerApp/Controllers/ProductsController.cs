using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using ServerApp.Context;
using ServerApp.Dtos;
using ServerApp.Models;
using System.Runtime.CompilerServices;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(SocialContext _context) : ControllerBase
    {


        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _context.Products.Select(product => new ResultProductDto { Id = product.Id, Name = product.Name, IsActive = product.IsActive, Price = product.Price }).ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.Select(product => new ResultProductDto { Id = product.Id, Name = product.Name, IsActive = product.IsActive, Price = product.Price }).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(CreateProductDto model)
        {
            _context.Add(new Product
            {
                Name = model.Name,
                IsActive = model.IsActive,
                Price = model.Price
                
            });
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync(UpdateProductDto model)
        {
            _context.Update(new Product
            {
                Id=model.Id,
                Name = model.Name,
                IsActive = model.IsActive,
                Price = model.Price
                
            });
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
