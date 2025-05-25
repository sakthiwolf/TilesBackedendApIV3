using Microsoft.AspNetCore.Mvc;
using Tiles.Core.DTO.ProductDto.CategoryrequestandSubcategoryrequest;
using Tiles.Core.ServiceContracts;

namespace TilesBackendApI.Controllers
{
    [ApiController]
    [Route("api/subcategories")]
    public class SubcategoriesController : ControllerBase
    {
        private readonly ISubcategoryService _service;

        public SubcategoriesController(ISubcategoryService service) => _service = service;

        // Create a new subcategory
        [HttpPost]
        public async Task<IActionResult> Create(SubcategoryRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { message = "Request body cannot be null." });

                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    return BadRequest(new { errors });
                }


                var subcategory = await _service.CreateSubcategoryAsync(request);

                // Return Created with route to get subcategories by CategoryId
                return CreatedAtAction(nameof(GetByCategoryId), new { categoryId = request.CategoryId }, subcategory);
            }
            catch (Exception ex)
            {
                // Log exception as needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Get all subcategories for a given category ID
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(Guid categoryId)
        {
            try
            {
                if (categoryId == Guid.Empty)
                    return BadRequest(new { message = "Category ID is required." });

                var subcategories = await _service.GetSubcategoriesByCategoryIdAsync(categoryId);
                return Ok(subcategories);
            }
            catch (Exception ex)
            {
                // Log exception as needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }
    }
}
