using InventoryApi.Entities;

namespace InventoryApi.Repositories.Interfaces;

public interface IBatchRepository : IRepository<Batch>
{
    Task<IEnumerable<Batch>> GetByProductIdAsync(Guid productId);
    Task<IEnumerable<(Batch Batch, Product Product)>> GetExpiredBatchesAsync();
    Task ClearExpiredBatchesAsync();
}
