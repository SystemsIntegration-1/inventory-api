using InventoryApi.Domain.Entities;

namespace InventoryApi.Infrastructure.Repositories.Interfaces;

public interface IInventoryRepository : IRepository<InventoryMovement>
{
    Task<IEnumerable<InventoryMovement>> GetMovementsByProductIdAsync(Guid productId);
}
