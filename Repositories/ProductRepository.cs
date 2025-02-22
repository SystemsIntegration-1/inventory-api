using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly InventoryDbContext _context;

    public ProductRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProductsAsync() => await _context.Products.ToListAsync();

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task AddInventoryMovementAsync(InventoryMovement movement)
    {
        _context.InventoryMovements.Add(movement);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<InventoryMovement>> GetMovementsByProductIdAsync(Guid productId) =>
        await _context.InventoryMovements.Where(m => m.ProductId == productId).ToListAsync();

    public async Task<List<Product>> SearchProductsAsync(string name) =>
        await _context
            .Products.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{name.ToLower()}%"))
            .ToListAsync();

    public async Task AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }
}
