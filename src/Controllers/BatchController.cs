using InventoryApi.Dto;
using InventoryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers;

[ApiController]
[Route("batches")]
public class BatchController : ControllerBase
{
    private readonly IBatchService _batchService;

    public BatchController(IBatchService batchService)
    {
        _batchService = batchService;
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetBatchesByProduct(Guid productId)
    {
        var batches = await _batchService.GetBatchesByProductIdAsync(productId);
        return Ok(batches);
    }

    [HttpPost]
    public async Task<IActionResult> AddBatch([FromBody] CreateBatchDto batchDto)
    {
        if (batchDto.ProductId == Guid.Empty)
        {
            return BadRequest("ProductId is required");
        }

        await _batchService.AddBatchAsync(batchDto);
        return Ok(new { message = "Batch added successfully" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBatch(Guid id, [FromBody] BatchDto batchDto)
    {
        var existingBatch = await _batchService.GetBatchByIdAsync(id);
        if (existingBatch == null)
            return NotFound("Batch not found");

        await _batchService.UpdateBatchAsync(id, batchDto);
        return NoContent();
    }
}
