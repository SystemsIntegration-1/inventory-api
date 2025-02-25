using InventoryApi.DBContext;
using InventoryApi.Entities;
using InventoryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _context.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(Guid id) =>
        await _context.Products.FindAsync(id);

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
        await _context.Products
            .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{name.ToLower()}%"))
            .ToListAsync();
}
