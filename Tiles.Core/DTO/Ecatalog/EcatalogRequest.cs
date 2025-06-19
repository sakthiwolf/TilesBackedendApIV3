using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.DTO.Ecatalog
{
    public class EcatalogRequest
    {
        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public string CatalogName { get; set; } = null!;

        [Required]
        public string CoverPhoto { get; set; } = null!;

        [Required]
        public string FileUrl { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public List<string>? Tags { get; set; }
    }
}
