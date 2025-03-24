using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUseSqlLiteItog.Models;

namespace WebApiUseSqlLiteItog.Repository
{
    public interface IProductLineRepository
    {
        Task<ActionResult<IEnumerable<Product>>> GetProducts();
        Task<ActionResult<Product>> PostProduct(Product item);
        Task<IActionResult> PutProduct(int id, Product item);
        Task<IActionResult> DeleteProduct(int id);
    }
}
