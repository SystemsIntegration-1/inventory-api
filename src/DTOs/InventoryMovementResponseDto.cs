namespace inventory_api.src.DTOs;

public class InventoryMovementResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid? MovementId { get; set; }
}
