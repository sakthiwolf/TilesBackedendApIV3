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

        [HttpPost]
        public async Task<IActionResult> Create( SubcategoryRequest request)
        {
            var subcategory = await _service.CreateSubcategoryAsync(request);
            return CreatedAtAction(nameof(GetByCategoryId), new { categoryId = request.CategoryId }, subcategory);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(Guid categoryId)
        {
            var subcategories = await _service.GetSubcategoriesByCategoryIdAsync(categoryId);
            return Ok(subcategories);
        }
    }
}
