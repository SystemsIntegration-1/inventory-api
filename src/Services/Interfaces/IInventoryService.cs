using InventoryApi.Dto;

namespace InventoryApi.Services.Interfaces;
public interface IInventoryService
{
    Task RegisterInventoryMovementAsync(InventoryMovementDto movementDto);
    Task<IEnumerable<InventoryMovementDto>> GetMovementsByProductIdAsync(Guid productId);
}
