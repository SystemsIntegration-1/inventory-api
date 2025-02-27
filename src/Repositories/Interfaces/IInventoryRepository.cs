using InventoryApi.Entities;

namespace InventoryApi.Repositories.Interfaces;

public interface IInventoryRepository : IRepository<InventoryMovement>
{
    Task<IEnumerable<InventoryMovement>> GetMovementsByProductIdAsync(Guid productId);
}
