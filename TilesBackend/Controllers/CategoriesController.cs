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

        [HttpPost]
        public async Task<IActionResult> Create( CategoryRequest request)
        {
            var category = await _service.CreateCategoryAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = category.Id }, category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
