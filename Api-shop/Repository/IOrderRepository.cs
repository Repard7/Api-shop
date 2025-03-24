using Microsoft.AspNetCore.Mvc;
using WebApiUseSqlLiteItog.Models;

namespace WebApiUseSqlLiteItog.Repository
{
    public interface IOrderRepository
    {
        Task<ActionResult<IEnumerable<OrderProduct>>> GetCompletedProducts();
        Task<ActionResult<CartProduct>> PostProductInCompleted(int id);
    }
}
