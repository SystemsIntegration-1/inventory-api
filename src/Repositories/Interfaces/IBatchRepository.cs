using inventory_api.src.Models;
using InventoryApi.Entities;

namespace InventoryApi.Repositories.Interfaces;

public interface IBatchRepository : IRepository<Batch>
{
    Task<IEnumerable<Batch>> GetByProductIdAsync(Guid productId);
    Task<IEnumerable<BatchProductPair>> GetExpiredBatchesAsync();
    Task ClearExpiredBatchesAsync();
}
