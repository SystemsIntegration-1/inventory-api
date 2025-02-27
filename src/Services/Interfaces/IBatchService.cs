using InventoryApi.Dto;

namespace InventoryApi.Services.Interfaces;

public interface IBatchService
{
    Task<BatchDto?> GetBatchByIdAsync(Guid id);
    Task<IEnumerable<BatchDto>> GetBatchesByProductIdAsync(Guid productId);
    Task AddBatchAsync(CreateBatchDto batchDto);
    Task UpdateBatchAsync(Guid id, BatchDto batchDto);
}
