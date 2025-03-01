namespace inventory_api.API.Models;

public class StockAllocationResult
{
    public bool Success { get; set; }
    public List<BatchAllocation> Allocations { get; set; } = new List<BatchAllocation>();
}
