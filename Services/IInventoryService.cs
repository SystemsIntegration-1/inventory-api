public interface IInventoryService
{
    Task<List<ProductDto>> GetProductsAsync();
    Task<ProductDto> GetProductByIdAsync(Guid id);
    Task RegisterInventoryMovementAsync(InventoryMovementDto movementDto);
    Task<List<InventoryMovementDto>> GetMovementsByProductIdAsync(Guid productId);
    Task UpdateProductAsync(ProductDto productDto);
    Task<List<ProductDto>> SearchProductsAsync(string name);
    Task<ProductDto> AddProductAsync(ProductDto productDto);
}
