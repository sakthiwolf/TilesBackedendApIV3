using Microsoft.AspNetCore.Mvc;
using Tiles.Core.DTO.Ecatalog;
using Tiles.Core.ServiceContracts;

namespace TilesBackendApI.Controllers
{
    [ApiController]
    [Route("api/ecatalogs")]
    public class EcatalogController : ControllerBase
    {
        private readonly IEcatalogService _service;

        public EcatalogController(IEcatalogService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EcatalogRequest request)
        {
            var result = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpGet("{categoryId?}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid? categoryId)
        {
            var result = await _service.GetAllAsync(categoryId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EcatalogRequest request)
        {
            var result = await _service.UpdateAsync(id, request);
            if (result == null) return NotFound(new { message = "Ecatalog not found" });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound(new { message = "Ecatalog not found" });
            return Ok(new { message = "Ecatalog deleted successfully" });
        }
    }
}
