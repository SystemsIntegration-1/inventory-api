using InventoryApi.Domain.Entities;
using InventoryApi.Infrastructure.DBContext;
using InventoryApi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Infrastructure.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly InventoryDbContext _context;

    public InventoryRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InventoryMovement>> GetAllAsync() =>
        await _context.InventoryMovements.ToListAsync();

    public async Task<InventoryMovement?> GetByIdAsync(Guid id) =>
        await _context.InventoryMovements.FindAsync(id);

    public async Task AddAsync(InventoryMovement movement)
    {
        _context.InventoryMovements.Add(movement);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(InventoryMovement movement)
    {
        _context.InventoryMovements.Update(movement);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<InventoryMovement>> GetMovementsByProductIdAsync(Guid productId)
    {
        return await _context.InventoryMovements.Where(m => m.ProductId == productId).ToListAsync();
    }
}
