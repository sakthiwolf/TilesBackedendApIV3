using Microsoft.AspNetCore.Mvc;
using Tiles.Core.DTO.UserDto;
using Tiles.Core.ServiceContracts.UserManagement.Application.Interfaces;

namespace TilesBackendApI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForgotController : ControllerBase
    {
        private readonly IUserService _userService;

        public ForgotController(IUserService userService)
        {
            _userService = userService;
        }

        // Forgot password - sends an OTP to the user's email
        [HttpPost("forgotPasswordEmail")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            try
            {
                // Check for null request
                if (dto == null)
                    return BadRequest(new { msg = "Request body cannot be null" });

                // Validate model state
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    return BadRequest(new { errors });
                }


                var result = await _userService.ForgotPasswordAsync(dto.Email);

                if (!result.Success)
                    return NotFound(new { msg = result.Message });

                return Ok(new { msg = "OTP sent successfully" });
            }
            catch (Exception ex)
            {
                // Log exception as needed

                return StatusCode(500, new { msg = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Verify OTP sent for password reset
        [HttpPost("verifyOtp")]
        public async Task<IActionResult> VerifyOtp(OtpVerifyDto dto)
        {
            try
            {
                // Check for null request
                if (dto == null)
                    return BadRequest(new { msg = "Request body cannot be null" });

                // Validate model state
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    return BadRequest(new { errors });
                }


                var result = await _userService.VerifyOtpAsync(dto.Email, dto.Otp);

                if (!result.Success)
                    return BadRequest(new { msg = result.Message });

                return Ok(new { msg = "OTP verified successfully" });
            }
            catch (Exception ex)
            {
                // Log exception as needed

                return StatusCode(500, new { msg = "An unexpected error occurred.", error = ex.Message });
            }
        }
    }
}
