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

        // Create a new product
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductRequest request)
        {
            try
            {
                // Null check for request body
                if (request == null)
                    return BadRequest(new { message = "Request body cannot be null" });

                // Model validation
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    return BadRequest(new { errors });
                }


                var id = await _service.AddProduct(request);
                return CreatedAtAction(nameof(GetProductById), new { id }, new { message = "Product created", id });
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Update an existing product by Id
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductUpdateRequest request)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest(new { message = "Product ID is required" });

                if (request == null)
                    return BadRequest(new { message = "Request body cannot be null" });

                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    return BadRequest(new { errors });
                }


                await _service.UpdateProduct(id, request);
                return Ok(new { message = "Product updated successfully" });
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Get all products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _service.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Get products filtered by categoryId and/or subCategoryId
        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered(Guid? categoryId, Guid? subCategoryId)
        {
            try
            {
                var products = await _service.GetProductsByFilter(categoryId, subCategoryId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Get product by Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest(new { message = "Product ID is required" });

                var product = await _service.GetProductById(id);
                return product == null ? NotFound(new { message = "Product not found" }) : Ok(product);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Delete product by Id
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest(new { message = "Product ID is required" });

                await _service.DeleteProduct(id);
                return Ok(new { message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }
    }
}
