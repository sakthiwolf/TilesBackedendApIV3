using System.ComponentModel.DataAnnotations;

namespace Tiles.Core.DTO.SellerDto
{
    public class SellerRequestDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be between 2 and 100 characters.", MinimumLength = 2)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Map link/location is required.")]
        public string Map { get; set; } = null!;

        [Required(ErrorMessage = "Whatsapp number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string WhatsappNumber { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Dealer name is required.")]
        [StringLength(100, ErrorMessage = "Dealer name must be between 2 and 100 characters.", MinimumLength = 2)]
        public string DealerName { get; set; } = null!;

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; } = null!;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public string Address { get; set; } = null!;
    }
}
