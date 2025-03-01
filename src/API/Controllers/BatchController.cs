using InventoryApi.API.Dto;
using InventoryApi.Aplication.Dto;
using InventoryApi.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.API.Controllers;

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

    [HttpGet("expired")]
    public async Task<IActionResult> GetExpiredBatches()
    {
        var expiredBatches = await _batchService.GetExpiredBatchesAsync();
        return Ok(expiredBatches);
    }

    [HttpPost("clear-expired")]
    public async Task<IActionResult> ClearExpiredBatches()
    {
        await _batchService.ClearExpiredBatchesAsync();
        return Ok(new { message = "Expired batches cleared successfully" });
    }
}
