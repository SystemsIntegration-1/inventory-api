namespace InventoryApi.Dto;

public class BatchDto
{
    public Guid ProductId { get; set; }
    public int Stock { get; set; }
    public long EntryDate { get; set; }
    public long ExpirationDate { get; set; }
}
