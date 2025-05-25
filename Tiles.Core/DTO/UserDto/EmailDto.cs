using System.ComponentModel.DataAnnotations;

namespace Tiles.Core.DTO.UserDto
{
    public class EmailDto
    {
        [Required(ErrorMessage = "Recipient email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string ToEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Recipient name is required.")]
        [StringLength(100, ErrorMessage = "Name can't exceed 100 characters.")]
        public string ToName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject is required.")]
        [StringLength(150, ErrorMessage = "Subject can't exceed 150 characters.")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email content is required.")]
        public string HtmlContent { get; set; } = string.Empty;
    }
}
