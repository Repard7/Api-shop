using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUseSqlLiteItog.Models;
using WebApiUseSqlLiteItog.Models.Context;
using WebApiUseSqlLiteItog.Repository;

namespace WebApiUseSqlLiteItog.Controllers
{
    [Route("api-shop")]
    [ApiController]
    public class CartController : Controller, ICartRepository
    {
        private readonly EFProductContext _context;

        public CartController(EFProductContext context)
        {
            _context = context;
        }

        //То, что могут делать только users
        [HttpGet, Route("cart")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IEnumerable<CartProduct>>> GetProductsInBasket()
        {
            return await _context.CartProducts.ToListAsync();
        }



        [HttpPost, Route("cart/{product_id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<CartProduct>> PostProductInBusket(int product_id)
        {
            Product product = _context.Products.Find(product_id);
            _context.CartProducts.Add(new CartProduct { Name = product.Name, Price = product.Price, Description = product.Description, Product_id = product.Id});
            await _context.SaveChangesAsync();
            return StatusCode(201, "Product add to card");
        }



        [HttpDelete("cart/{id}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteProductInBasket(int id)
        {
            var product = await _context.CartProducts.FindAsync(id);
            if (product == null)
            {
                return StatusCode(403, "Forbidden for you");
            }

            _context.CartProducts.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Item removed from cart");
        }



    }
}
