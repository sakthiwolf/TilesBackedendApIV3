using Microsoft.AspNetCore.Mvc;
using Tiles.Core.DTO.UserDto;
using Tiles.Core.ServiceContracts.UserManagement.Application.Interfaces;

namespace TilesBackendApI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class authController : ControllerBase
    {
        private readonly IUserService _userService;

        public authController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRequestDto dto)
        {
            try
            {
                if (dto is null)
                    return BadRequest(new { msg = "Request body cannot be null" });

                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    return BadRequest(new { errors });
                }



                var result = await _userService.RegisterUserAsync(dto);

                if (!result.Success)
                    return BadRequest(new { msg = result.Message });

                return Created("", new { msg = result.Message });
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return StatusCode(500, new { msg = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(string search = "", int pageNo = 1, int rowsPerPage = 10)
        {
            try
            {
                var result = await _userService.GetUsersAsync(search, pageNo, rowsPerPage);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);

                if (user is null)
                    return NotFound(new { msg = "User not found" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(Guid id, UserRequestDto dto)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest(new { msg = "User ID is required" });

                if (dto == null)
                    return BadRequest(new { msg = "Request body cannot be null" });

                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    return BadRequest(new { errors });
                }


                var result = await _userService.UpdateUserAsync(id, dto);

                if (!result.Success)
                    return NotFound(new { msg = result.Message });

                return Ok(new { msg = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);

                if (!result.Success)
                    return NotFound(new { msg = result.Message });

                return Ok(new { msg = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                if (dto is null)
                    return BadRequest(new { msg = "Request body cannot be null" });

                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    return BadRequest(new { errors });
                }


                var result = await _userService.LoginAsync(dto);

                if (!result.Success)
                    return Unauthorized(new { msg = result.Message });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpPut("updatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto dto)
        {
            try
            {
                if (dto is null)
                    return BadRequest(new { msg = "Request body cannot be null" });

                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    return BadRequest(new { errors });
                }


                var result = await _userService.UpdatePasswordAsync(dto);

                if (!result.Success)
                    return BadRequest(new { msg = result.Message });

                return Ok(new { msg = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "An unexpected error occurred.", error = ex.Message });
            }
        }
    }
}
