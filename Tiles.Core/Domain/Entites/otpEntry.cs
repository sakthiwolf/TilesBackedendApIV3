using System;
using System.ComponentModel.DataAnnotations;

namespace Tiles.Core.Domain.Entities
{
    public class OtpEntry
    {
 
        public string Otp { get; set; } = string.Empty;

        
        public DateTime Expiry { get; set; }
    }
}
