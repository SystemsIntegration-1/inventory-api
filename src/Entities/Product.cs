namespace InventoryApi.Entities;
public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public int AvailableQuantity { get; set; }
    public required string WarehouseLocation { get; set; }
    public long EntryDate { get; set; }
    public long ExpirationDate { get; set; }
}
