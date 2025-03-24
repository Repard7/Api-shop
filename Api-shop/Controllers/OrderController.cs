using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiUseSqlLiteItog.Models;
using WebApiUseSqlLiteItog.Models.Context;
using WebApiUseSqlLiteItog.Repository;

namespace WebApiUseSqlLiteItog.Controllers
{
    [Route("api-shop")]
    [ApiController]
    public class OrderController : Controller, IOrderRepository
    {
        private readonly EFProductContext _context;

        public OrderController(EFProductContext context)
        {
            _context = context;
        }

        //То, что могут делать только users
        [HttpGet, Route("order")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetCompletedProducts()
        {
            return await _context.OrderProducts.ToListAsync();
        }



        [HttpPost, Route("order/{id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<CartProduct>> PostProductInCompleted(int id)
        {
            HttpClient httpClient = new HttpClient();
            CartProduct product = _context.CartProducts.Find(id);
            if(product == null)
            {
                return StatusCode(422, "Cart is empty");
            }
            _context.OrderProducts.Add(new OrderProduct { Name = product.Name, Price = product.Price, Description = product.Description});
            await _context.SaveChangesAsync();
            //return CreatedAtAction("GetHelpPost", new { id = product.Id }, $"");
            return StatusCode(201, "Order is processed");
        }
    }
}
