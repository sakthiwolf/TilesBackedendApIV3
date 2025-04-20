using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.Domain.Entites
{
    public class User
    {
        public Guid Id { get; set; }
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public bool IsFirst { get; set; } = true;
        public string? Otp { get; set; }
        public DateTime? OtpExpiry { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
