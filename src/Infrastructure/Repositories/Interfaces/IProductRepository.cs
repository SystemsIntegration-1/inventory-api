using InventoryApi.Domain.Entities;

namespace InventoryApi.Infrastructure.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> SearchProductsAsync(string name);
    Task AddBatchAsync(Batch products);
    Task<Product?> GetByIdWithBatchesAsync(Guid id);
}
