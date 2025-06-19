using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.DTO.Instagram
{
    public class InstagramResponse
    {
        public Guid Id { get; set; }
        public string File { get; set; } = null!;
        public string CoverPhoto { get; set; } = null!;
    }
}
