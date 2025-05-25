using System.ComponentModel.DataAnnotations;

namespace Tiles.Core.DTO.UserDto
{
    public class OtpVerifyDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "OTP is required.")]
        [StringLength(6, MinimumLength = 4, ErrorMessage = "OTP must be between 4 and 6 characters.")]
        public string Otp { get; set; } = string.Empty;
    }
}
