using Microsoft.EntityFrameworkCore;
using test_api.Entites;
using test_api.Models;

namespace test_api.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            
        }
       // public DbSet<Product> Products { get; set; } = null!;
       public DbSet<Hero> Heroes { get; set; }
    }
}
