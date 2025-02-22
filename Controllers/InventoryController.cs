using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("inventory")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _service;

    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts() =>
        Ok(await _service.GetProductsAsync());

    [HttpGet("products/{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
    {
        var product = await _service.GetProductByIdAsync(id);
        return product != null ? Ok(product) : NotFound();
    }

    [HttpPost("movements")]
    public async Task<ActionResult> RegisterInventoryMovement(
        [FromBody] InventoryMovementDto movementDto
    )
    {
        await _service.RegisterInventoryMovementAsync(movementDto);
        return NoContent();
    }

    [HttpGet("products/search")]
    public async Task<IActionResult> SearchProducts([FromQuery] string name)
    {
        var products = await _service.SearchProductsAsync(name);
        return Ok(products);
    }

    [HttpGet("movements/{productId}")]
    public async Task<IActionResult> GetMovements(Guid productId)
    {
        var movements = await _service.GetMovementsByProductIdAsync(productId);
        return Ok(movements);
    }

    [HttpPost("products")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
    {
        var newProduct = await _service.AddProductAsync(productDto);
        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("products/{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductDto productDto)
    {
        if (id != productDto.Id)
            return BadRequest("Mismatched product ID");

        await _service.UpdateProductAsync(productDto);
        return NoContent();
    }
}
