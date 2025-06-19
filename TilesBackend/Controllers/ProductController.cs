using Microsoft.AspNetCore.Mvc;
using Tiles.Core.DTO.ProductDto;
using Tiles.Core.ServiceContracts;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductRequest request)
    {
        var result = await _service.CreateProductAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid? categoryId, [FromQuery] Guid? subCategoryId, [FromQuery] int page = 1, [FromQuery] int limit = 20)
    {
        var products = await _service.GetProductsAsync(categoryId, subCategoryId, page, limit);
        var count = await _service.CountProductsAsync(categoryId, subCategoryId);
        return Ok(new { message = "Products found", products, total = count, page, pages = (int)Math.Ceiling((double)count / limit) });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateRequest request)
    {
        var result = await _service.UpdateProductAsync(id, request);
        if (result == null) return NotFound(new { message = "Product not found" });
        return Ok(new { message = "Product updated successfully", product = result });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteProductAsync(id);
        if (!deleted) return NotFound(new { message = "Product not found" });
        return Ok(new { message = "Product deleted successfully" });
    }
}
