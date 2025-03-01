using inventory_api.Aplication.DTOs;
using InventoryApi.API.Dto;
using InventoryApi.Aplication.Dto;

namespace InventoryApi.Aplication.Services.Interfaces;

public interface IBatchService
{
    Task<BatchDto?> GetBatchByIdAsync(Guid id);
    Task<IEnumerable<BatchDto>> GetBatchesByProductIdAsync(Guid productId);
    Task AddBatchAsync(CreateBatchDto batchDto);
    Task UpdateBatchAsync(Guid id, BatchDto batchDto);
    Task<IEnumerable<ExpiredBatchDto>> GetExpiredBatchesAsync();
    Task ClearExpiredBatchesAsync();
}
