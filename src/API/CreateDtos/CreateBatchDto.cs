using System.ComponentModel.DataAnnotations;

namespace InventoryApi.API.Dto;

public class CreateBatchDto
{
    public Guid ProductId { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Batch stock cannot be 0")]
    public int Stock { get; set; }
    public long EntryDate { get; set; }
    public long ExpirationDate { get; set; }
}
