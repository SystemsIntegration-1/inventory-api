namespace InventoryApi.Aplication.Dto;

public class InventoryMovementDto
{
    public Guid ProductId { get; set; }
    public required string MovementType { get; set; }
    public int Quantity { get; set; }
    public long MovementDate { get; set; }
    public required string Origin { get; set; }
    public required string Destination { get; set; }
}
