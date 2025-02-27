namespace InventoryApi.Dto;

public class CreateProductDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required string WarehouseLocation { get; set; }
}
