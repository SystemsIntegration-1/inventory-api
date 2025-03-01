using InventoryApi.Domain.Entities;

namespace inventory_api.API.Models;

public class BatchAllocation
{
    public required Batch Batch { get; set; }
    public int QuantityToTake { get; set; }
}
