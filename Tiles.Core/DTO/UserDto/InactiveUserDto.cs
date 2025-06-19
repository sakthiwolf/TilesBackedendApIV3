using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.DTO.UserDto
{
    public class InactiveUserDto
    {
        public string Msg { get; set; } = "User account is inactive";
        public bool IsActive { get; set; } = false;
    }
}
