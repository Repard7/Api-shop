using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUseSqlLiteItog.Models;
using WebApiUseSqlLiteItog.Models.Context;
using WebApiUseSqlLiteItog.Repository;

namespace WebApiUseSqlLiteItog.Controllers
{
    [Route("api-shop")]
    [ApiController]
    public class ProductController : ControllerBase,IProductLineRepository
    {
        private readonly EFProductContext _context;

        public ProductController(EFProductContext context)
        {
            _context = context;
        }

        //То, что могут делать все
        [HttpGet, Route("products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }






        //То, что может делать только администратор
        [HttpPost, Route("product")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return StatusCode(201, "Product added");
        }



        [HttpPut("product/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            var entity = _context.Products.FirstOrDefault(item => item.Id == id);
            if (entity==null)
            {
                return BadRequest();
            }
            entity.Name = product.Name;
            entity.Price = product.Price;
            entity.Description = product.Description;
            _context.Products.Update(entity);
            _context.SaveChanges();
            return Ok(entity);

        }



        [HttpDelete("product/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return StatusCode(403, "Forbidden for you");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Product removed");
        }


    }
}
