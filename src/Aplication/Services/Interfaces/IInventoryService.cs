using inventory_api.Aplication.DTOs;
using InventoryApi.Aplication.Dto;

namespace InventoryApi.Aplication.Services.Interfaces;

public interface IInventoryService
{
    Task<InventoryMovementResponseDto> RegisterInventoryMovementAsync(InventoryMovementDto movementDto);
    Task<IEnumerable<InventoryMovementDto>> GetMovementsByProductIdAsync(Guid productId);
    Task<IEnumerable<InventoryMovementDto>> GetAllMovementsAsync();
}
