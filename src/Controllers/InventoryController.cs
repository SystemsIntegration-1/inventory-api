using InventoryApi.Dto;
using InventoryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers;

[ApiController]
[Route("inventory")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _service;

    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

    [HttpPost("movements")]
    public async Task<IActionResult> RegisterInventoryMovement([FromBody] InventoryMovementDto movementDto)
    {
        await _service.RegisterInventoryMovementAsync(movementDto);
        return NoContent();
    }

    [HttpGet("movements/{productId}")]
    public async Task<IActionResult> GetMovements(Guid productId)
    {
        var movements = await _service.GetMovementsByProductIdAsync(productId);
        return Ok(movements);
    }
}
