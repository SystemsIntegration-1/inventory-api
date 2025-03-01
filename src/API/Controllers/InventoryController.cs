using InventoryApi.Aplication.Dto;
using InventoryApi.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.API.Controllers;

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
        var result = await _service.RegisterInventoryMovementAsync(movementDto);
        
        if (!result.Success)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }

    [HttpGet("movements/{productId}")]
    public async Task<IActionResult> GetMovements(Guid productId)
    {
        var movements = await _service.GetMovementsByProductIdAsync(productId);
        return Ok(movements);
    }
    
    [HttpGet("movements")]
    public async Task<IActionResult> GetAllMovements()
    {
        var movements = await _service.GetAllMovementsAsync();
        return Ok(movements);
    }
}
