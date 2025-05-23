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

            // Constructor to inject the user service dependency
            public authController(IUserService userService)
            {
                _userService = userService;
            }

            // Register a new user
            [HttpPost("register")]
            public async Task<IActionResult> RegisterUser(UserRequestDto dto)
            {
                var result = await _userService.RegisterUserAsync(dto);

                // Check if registration was successful, return appropriate response
                if (!result.Success)
                    return BadRequest(new { msg = result.Message });

                return Created("", new { msg = result.Message });
            }

            // Get a list of users with pagination and optional search filter
            [HttpGet]
            public async Task<IActionResult> GetUsers( string search = "",  int pageNo = 1, int rowsPerPage = 10)
            {
                var result = await _userService.GetUsersAsync(search, pageNo, rowsPerPage);
                return Ok(result);
            }

            // Get user by ID
            [HttpGet("{id}")]
            public async Task<IActionResult> GetUserById(Guid id)
            {
                var user = await _userService.GetUserByIdAsync(id);

                // Return 404 if user not found
                if (user == null)
                    return NotFound(new { msg = "User not found" });

                return Ok(user);
            }

        // Edit an existing user by their ID
           [HttpPut("{id}")]
          public async Task<IActionResult> EditUser(Guid id, UserRequestDto dto)
        {
            if (id == Guid.Empty)
                return BadRequest(new { msg = "User ID is required" });

            var result = await _userService.UpdateUserAsync(id, dto);

            if (!result.Success)
                return NotFound(new { msg = result.Message });

            return Ok(new { msg = result.Message });
        }


          // Delete a user by their ID
          [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteUser(Guid id)
            {
                var result = await _userService.DeleteUserAsync(id);

                // Return 404 if deletion failed
                if (!result.Success)
                    return NotFound(new { msg = result.Message });

                return Ok(new { msg = result.Message });
            }

            // User login with email and password
            [HttpPost("login")]
            public async Task<IActionResult> Login( LoginDto dto)
            {
                var result = await _userService.LoginAsync(dto);

                // Return Unauthorized if login fails
                if (!result.Success)
                    return Unauthorized(new { msg = result.Message });

                return Ok(result.Data);
            }

            // Update user password
            [HttpPut("updatePassword")]
            public async Task<IActionResult> UpdatePassword(UpdatePasswordDto dto)
            {
                var result = await _userService.UpdatePasswordAsync(dto);

                // Return BadRequest if password update fails
                if (!result.Success)
                    return BadRequest(new { msg = result.Message });

                return Ok(new { msg = result.Message });
            }

           


        }


}


