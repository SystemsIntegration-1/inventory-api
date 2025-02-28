using inventory_api.src.DTOs;
using InventoryApi.Dto;

namespace InventoryApi.Services.Interfaces;

public interface IInventoryService
{
    Task<InventoryMovementResponseDto> RegisterInventoryMovementAsync(InventoryMovementDto movementDto);
    Task<IEnumerable<InventoryMovementDto>> GetMovementsByProductIdAsync(Guid productId);
    Task<IEnumerable<InventoryMovementDto>> GetAllMovementsAsync();
}
