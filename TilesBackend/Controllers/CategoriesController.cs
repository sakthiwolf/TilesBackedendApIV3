using Microsoft.AspNetCore.Mvc;
using Tiles.Core.DTO.ProductDto.CategoryrequestandSubcategoryrequest;
using Tiles.Core.ServiceContracts;

namespace TilesBackendApI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service) => _service = service;

        // Create a new category
        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            try
            {
                // Check if request body is null
                if (request == null)
                    return BadRequest(new { msg = "Request body cannot be null" });

                // Validate the model state (data annotations)
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var category = await _service.CreateCategoryAsync(request);

                // Return 201 Created with the created category data and location header
                return CreatedAtAction(nameof(GetAll), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                // Log exception if needed

                // Return generic 500 error message with exception details
                return StatusCode(500, new { msg = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Get all categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _service.GetCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                // Log exception if needed

                // Return generic 500 error message with exception details
                return StatusCode(500, new { msg = "An unexpected error occurred.", error = ex.Message });
            }
        }
    }
}
