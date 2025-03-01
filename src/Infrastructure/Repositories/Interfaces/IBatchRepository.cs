using inventory_api.API.Models;
using InventoryApi.Domain.Entities;

namespace InventoryApi.Infrastructure.Repositories.Interfaces;

public interface IBatchRepository : IRepository<Batch>
{
    Task<IEnumerable<Batch>> GetByProductIdAsync(Guid productId);
    Task<IEnumerable<BatchProductPair>> GetExpiredBatchesAsync();
    Task ClearExpiredBatchesAsync();
}
