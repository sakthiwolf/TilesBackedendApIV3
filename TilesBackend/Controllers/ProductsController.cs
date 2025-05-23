using Microsoft.AspNetCore.Mvc;
using Tiles.Core.DTO.ProductDto;
using Tiles.Core.ServiceContracts;

namespace Tiles.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductRequest request)
        {
            var id = await _service.AddProduct(request);
            return CreatedAtAction(nameof(GetProductById), new { id }, new { message = "Product created", id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductUpdateRequest request)
        {
            await _service.UpdateProduct(id, request);
            return Ok(new { message = "Product updated successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _service.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered( Guid? categoryId, Guid? subCategoryId)
        {
            var products = await _service.GetProductsByFilter(categoryId, subCategoryId);
            return Ok(products);
        }
         
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _service.GetProductById(id);
            return product == null ? NotFound(new { message = "Product not found" }) : Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _service.DeleteProduct(id);
            return Ok(new { message = "Product deleted successfully" });
        }
    }
}
