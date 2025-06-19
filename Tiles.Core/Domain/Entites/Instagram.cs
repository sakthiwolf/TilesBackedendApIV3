using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.Domain.Entites
{
    public class Instagram
    {
        public Guid Id { get; set; }

        public string File { get; set; } = null!;       // Media URL (image/video)

        public string CoverPhoto { get; set; } = null!; // Cover photo URL
    }
}
