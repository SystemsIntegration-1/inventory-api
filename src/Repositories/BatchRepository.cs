using inventory_api.src.Models;
using InventoryApi.DBContext;
using InventoryApi.Entities;
using InventoryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Repositories;

public class BatchRepository : IBatchRepository
{
    private readonly ProductDbContext _context;

    public BatchRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Batch>> GetAllAsync() => await _context.Batches.ToListAsync();

    public async Task<Batch?> GetByIdAsync(Guid id) => await _context.Batches.FindAsync(id);

    public async Task<IEnumerable<Batch>> GetByProductIdAsync(Guid productId) =>
        await _context.Batches.Where(b => b.ProductId == productId).ToListAsync();

    public async Task AddAsync(Batch batch)
    {
        _context.Batches.Add(batch);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Batch batch)
    {
        _context.Batches.Update(batch);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<BatchProductPair>> GetExpiredBatchesAsync()
    {
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var expiredBatches = await _context.Batches
            .Where(b => b.ExpirationDate < currentTime && b.Stock > 0)
            .ToListAsync();

        var result = new List<BatchProductPair>();

        foreach (var batch in expiredBatches)
        {
            var product = await _context.Products.FindAsync(batch.ProductId);
            if (product != null)
            {
                result.Add(new BatchProductPair
                {
                    Batch = batch,
                    Product = product
                });
            }
        }

        return result;
    }

    public async Task ClearExpiredBatchesAsync()
    {
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var expiredBatches = await _context.Batches
        .Where(b => b.ExpirationDate < currentTime && b.Stock > 0)
        .ToListAsync();

        _context.Batches.RemoveRange(expiredBatches);
        await _context.SaveChangesAsync();
    }
}
