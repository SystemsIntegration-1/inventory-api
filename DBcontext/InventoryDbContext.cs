using Microsoft.EntityFrameworkCore;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<InventoryMovement> InventoryMovements { get; set; }
}
