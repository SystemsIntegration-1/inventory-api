using InventoryApi.API.Dto;
using InventoryApi.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.API.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductDto productDto)
    {
        await _productService.AddProductAsync(productDto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] CreateProductDto productDto)
    {
        var existingProduct = await _productService.GetProductByIdAsync(id);
        if (existingProduct == null)
            return NotFound("Product not found");

        await _productService.UpdateProductAsync(id, productDto);

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts([FromQuery] string name)
    {
        var products = await _productService.SearchProductsAsync(name);
        return Ok(products);
    }
}
