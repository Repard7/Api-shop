using Microsoft.EntityFrameworkCore;

namespace WebApiUseSqlLiteItog.Models.Context
{
    public class EFProductContext : DbContext
    {
        public EFProductContext(DbContextOptions<EFProductContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuider)
        {
            base.OnModelCreating(modelBuider);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
