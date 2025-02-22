public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public int AvailableQuantity { get; set; }
    public string WarehouseLocation { get; set; }
    public long EntryDate { get; set; }
    public long ExpirationDate { get; set; }
}
