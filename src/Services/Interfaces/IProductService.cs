using InventoryApi.Dto;

namespace InventoryApi.Services.Interfaces;
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync();
    Task<ProductDto> GetProductByIdAsync(Guid id);
    Task AddProductAsync(CreateProductDto productDto);
    Task UpdateProductAsync(Guid id, CreateProductDto productDto);
    Task<IEnumerable<ProductDto>> SearchProductsAsync(string name);
}
