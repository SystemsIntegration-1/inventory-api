namespace InventoryApi.Entities;

public class InventoryMovement
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public required string MovementType { get; set; }
    public int Quantity { get; set; }
    public long MovementDate { get; set; }
    public required string Origin { get; set; }
    public required string Destination { get; set; }
}
