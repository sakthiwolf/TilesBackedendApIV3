using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.Domain.Entites
{
    public class OtpEntry
    {
        public string Otp { get; set; } = string.Empty;
        public DateTime Expiry { get; set; }
    }

}
