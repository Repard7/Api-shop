using Microsoft.AspNetCore.Mvc;
using WebApiUseSqlLiteItog.Models;

namespace WebApiUseSqlLiteItog.Repository
{
    public interface ICartRepository
    {
        Task<ActionResult<IEnumerable<CartProduct>>> GetProductsInBasket();
        Task<IActionResult> DeleteProductInBasket(int id);
        Task<ActionResult<CartProduct>> PostProductInBusket(int product_id);
    }
}
