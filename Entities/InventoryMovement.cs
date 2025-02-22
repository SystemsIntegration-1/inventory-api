public class InventoryMovement
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string MovementType { get; set; }
    public int Quantity { get; set; }
    public long MovementDate { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
}
