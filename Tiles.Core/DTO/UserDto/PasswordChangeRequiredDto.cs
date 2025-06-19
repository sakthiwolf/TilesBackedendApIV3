using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.DTO.UserDto
{
    public class PasswordChangeRequiredDto
    {
        public string Msg { get; set; } = "Password change required";
        public bool IsFirst { get; set; } = true;
    }
}
