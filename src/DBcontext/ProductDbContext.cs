using InventoryApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.DBContext;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
}
