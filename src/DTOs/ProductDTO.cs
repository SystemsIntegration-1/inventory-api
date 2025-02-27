namespace InventoryApi.Dto;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string WarehouseLocation { get; set; } = string.Empty;
    public List<BatchDto> Batches { get; set; } = new();
}
