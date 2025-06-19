using Microsoft.AspNetCore.Mvc;
using Tiles.Core.DTO.Instagram;

using Tiles.Core.ServiceContracts;

namespace Tiles.API.Controllers
{
    [ApiController]
    [Route("api/instagram")]
    public class InstagramController : ControllerBase
    {
        private readonly IInstagramService _service;

        public InstagramController(IInstagramService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InstagramRequest request)
        {
            var result = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] InstagramRequest request)
        {
            var result = await _service.UpdateAsync(id, request);
            if (result == null) return NotFound(new { message = "Instagram post not found" });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound(new { message = "Instagram post not found" });
            return Ok(new { message = "Instagram post deleted successfully" });
        }
    }
}
