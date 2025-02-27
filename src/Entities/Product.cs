namespace InventoryApi.Entities;

public class Product
{
    public Guid Id { get; set; }
    public Guid SharedId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required string WarehouseLocation { get; set; }
    public List<Batch> Batches { get; set; } = new();
}
