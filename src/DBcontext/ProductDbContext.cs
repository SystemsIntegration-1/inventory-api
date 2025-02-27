using InventoryApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.DBContext;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Batch> Batches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Product>()
            .HasMany(p => p.Batches)
            .WithOne()
            .HasForeignKey("ProductId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
