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

        // Get all sellers with optional search and pagination
        [HttpGet]
        public async Task<IActionResult> GetAll(string? search = "", int pageNo = 1, int rowsPerPage = 10)
        {
            try
            {
                var (sellers, total) = await _service.GetAllAsync(search ?? "", pageNo, rowsPerPage);

                return Ok(new
                {
                    data = sellers,
                    metadata = new
                    {
                        search,
                        pageNo,
                        rowsPerPage,
                        totalPages = Math.Ceiling((double)total / rowsPerPage),
                        totalRecords = total
                    }
                });
            }
            catch (Exception ex)
            {
                // Log exception if needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Get a seller by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest(new { message = "Seller ID is required." });

                var seller = await _service.GetByIdAsync(id);
                if (seller == null)
                    return NotFound(new { message = "Seller not found." });

                return Ok(seller);
            }
            catch (Exception ex)
            {
                // Log exception if needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Create a new seller
        [HttpPost]
        public async Task<IActionResult> Create(SellerRequestDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Request body cannot be null." });

                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    return BadRequest(new { errors });
                }


                var seller = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = seller.Id }, seller);
            }
            catch (Exception ex)
            {
                // Log exception if needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Update an existing seller by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, SellerRequestDto dto)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest(new { message = "Seller ID is required." });

                if (dto == null)
                    return BadRequest(new { message = "Request body cannot be null." });

                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    return BadRequest(new { errors });
                }


                var seller = await _service.UpdateAsync(id, dto);

                if (seller == null)
                    return NotFound(new { message = "Seller not found." });

                return Ok(seller);
            }
            catch (Exception ex)
            {
                // Log exception if needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Delete a seller by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest(new { message = "Seller ID is required." });

                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception if needed
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }
    }
}
