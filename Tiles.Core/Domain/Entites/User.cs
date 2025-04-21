using System;

namespace Tiles.Core.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public int SerialNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsFirst { get; set; } = true;
        public string? Otp { get; set; }

        // Ensure this is set with DateTime.UtcNow
        public DateTime? OtpExpiry { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
