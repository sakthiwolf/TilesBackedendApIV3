using Microsoft.AspNetCore.Mvc;
using Tiles.Core.DTO.UserDto;
using Tiles.Core.ServiceContracts.UserManagement.Application.Interfaces;

namespace TilesBackendApI.Controllers
{
    [ApiController]
    [Route("api/forgot")]
    public class ForgotController : ControllerBase
    {
        private readonly IUserService _userService;

        public ForgotController(IUserService userService)
        {
            _userService = userService;
        }

        // Forgot password - sends an OTP to the user's email
        [HttpPost("forgotPasswordEmail")]
        public async Task<IActionResult> ForgotPassword( ForgotPasswordDto dto)
        {
            var result = await _userService.ForgotPasswordAsync(dto.Email);

            if (!result.Success)
                return NotFound(new { msg = result.Message });

            return Ok(new { msg = "OTP sent successfully" });
        }

        // Verify OTP sent for password reset
        [HttpPost("verifyOtp")]
        public async Task<IActionResult> VerifyOtp(OtpVerifyDto dto)
        {
            var result = await _userService.VerifyOtpAsync(dto.Email, dto.Otp);

            if (!result.Success)
                return BadRequest(new { msg = result.Message });

            return Ok(new { msg = "OTP verified successfully" });
        }
    }
}
