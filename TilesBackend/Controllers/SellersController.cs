using Microsoft.AspNetCore.Mvc;
using Tiles.Core.DTO.SellerDto;
using Tiles.Core.ServiceContracts;

namespace Tiles.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellersController : ControllerBase
    {
        private readonly ISellerService _service;

        public SellersController(ISellerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll( string? search = "",  int pageNo = 1,  int rowsPerPage = 10)
        {
            var (sellers, total) = await _service.GetAllAsync(search ?? "", pageNo, rowsPerPage);
            return Ok(new
            {
                data = sellers,
                metadata = new { search, pageNo, rowsPerPage, totalPages = Math.Ceiling((double)total / rowsPerPage), totalRecords = total }
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var seller = await _service.GetByIdAsync(id);
            return Ok(seller);
        }

        [HttpPost]
        public async Task<IActionResult> Create( SellerRequestDto dto)
        {
            var seller = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = seller.Id }, seller);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,  SellerRequestDto dto)
        {
            var seller = await _service.UpdateAsync(id, dto);
            return Ok(seller);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
