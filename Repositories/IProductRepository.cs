public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(Guid id);
    Task AddInventoryMovementAsync(InventoryMovement movement);
    Task UpdateProductAsync(Product product);
    Task<List<InventoryMovement>> GetMovementsByProductIdAsync(Guid productId);
    Task<List<Product>> SearchProductsAsync(string name);
    Task AddProductAsync(Product product);
}
