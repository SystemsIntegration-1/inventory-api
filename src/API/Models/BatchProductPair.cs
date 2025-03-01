using InventoryApi.Domain.Entities;

namespace inventory_api.API.Models;

public class BatchProductPair
{
    public required Batch Batch { get; set; }
    public required Product Product { get; set; }
}
