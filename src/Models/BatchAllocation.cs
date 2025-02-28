using InventoryApi.Entities;

namespace inventory_api.src.Models;

public class BatchAllocation
{
    public required Batch Batch { get; set; }
    public int QuantityToTake { get; set; }
}
