using Microsoft.AspNetCore.Mvc;
using Tiles.Core.DTO;
using Tiles.Core.ServiceContracts.UserManagement.Application.Interfaces;

namespace TilesBackendApI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequestDto dto)
        {
            var result = await _userService.RegisterUserAsync(dto);
            if (!result.Success)
                return BadRequest(new { msg = result.Message });

            return Created("", new { msg = result.Message });
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] string search = "", [FromQuery] int pageNo = 1, [FromQuery] int rowsPerPage = 10)
        {
            var result = await _userService.GetUsersAsync(search, pageNo, rowsPerPage);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { msg = "User not found" });

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(Guid id, [FromBody] UserRequestDto dto)
        {
            var result = await _userService.UpdateUserAsync(id, dto);
            if (!result.Success)
                return NotFound(new { msg = result.Message });

            return Ok(new { msg = result.Message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result.Success)
                return NotFound(new { msg = result.Message });

            return Ok(new { msg = result.Message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _userService.LoginAsync(dto);
            if (!result.Success)
                return Unauthorized(new { msg = result.Message });

            return Ok(result.Data);
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto)
        {
            var result = await _userService.UpdatePasswordAsync(dto);
            if (!result.Success)
                return BadRequest(new { msg = result.Message });

            return Ok(new { msg = result.Message });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email)
        {
            var result = await _userService.ForgotPasswordAsync(email);
            return Ok(result);
        }

        //[HttpPost("forgot-password")]
        //public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        //{
        //    var result = await _userService.SendOtpAsync(dto.Email);
        //    if (!result.Success)
        //        return BadRequest(new { msg = result.Message });

        //    return Ok(new { msg = result.Message });
        //}

        //[HttpPost("verify-otp")]
        //public async Task<IActionResult> VerifyOtp([FromBody] OtpVerifyDto dto)
        //{
        //    var result = await _userService.VerifyOtpAsync(dto.Email, dto.Otp);
        //    if (!result.Success)
        //        return BadRequest(new { msg = result.Message });

        //    return Ok(new { msg = result.Message });
        //}

        // POST api/email/send
        //[HttpPost("send")]
        //public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
        //{
        //    if (string.IsNullOrEmpty(request.To) || string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body))
        //    {
        //        return BadRequest("To, Subject, and Body are required.");
        //    }

        //    try
        //    {
        //        await _userService.SendEmailAsync(request.To, request.Subject, request.Body);
        //        return Ok("Email sent successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


    }
}

