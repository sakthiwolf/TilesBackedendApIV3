using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.DTO.Instagram
{
    public class InstagramRequest
    {
        [Required]
        public string File { get; set; } = null!;

        [Required]
        public string CoverPhoto { get; set; } = null!;
    }
}
