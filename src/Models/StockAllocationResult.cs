namespace inventory_api.src.Models;

public class StockAllocationResult
{
    public bool Success { get; set; }
    public List<BatchAllocation> Allocations { get; set; } = new List<BatchAllocation>();
}
