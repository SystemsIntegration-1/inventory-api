using InventoryApi.Entities;

namespace inventory_api.src.Models;

public class BatchProductPair
{
    public required Batch Batch { get; set; }
    public required Product Product { get; set; }
}
