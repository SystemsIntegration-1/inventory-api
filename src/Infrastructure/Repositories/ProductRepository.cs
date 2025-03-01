using InventoryApi.Domain.Entities;
using InventoryApi.Infrastructure.DBContext;
using InventoryApi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(Guid id) => await _context.Products.FindAsync(id);

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string name) =>
        await _context
            .Products.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{name.ToLower()}%"))
            .ToListAsync();

    public async Task AddBatchAsync(Batch batch)
    {
        _context.Batches.Add(batch);
        await _context.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdWithBatchesAsync(Guid id)
    {
        return await _context.Products.Include(p => p.Batches).FirstOrDefaultAsync(p => p.Id == id);
    }
}
