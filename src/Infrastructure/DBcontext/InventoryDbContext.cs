using InventoryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Infrastructure.DBContext;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options) { }

    public DbSet<InventoryMovement> InventoryMovements { get; set; }
}
