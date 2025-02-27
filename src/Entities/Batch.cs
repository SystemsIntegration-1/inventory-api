namespace InventoryApi.Entities;

public class Batch
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Stock { get; set; }
    public long EntryDate { get; set; }
    public long ExpirationDate { get; set; }
}
