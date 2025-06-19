using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.DTO.Ecatalog
{
    public class EcatalogResponse
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string CatalogName { get; set; } = null!;
        public string CoverPhoto { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public bool IsActive { get; set; }
        public List<string>? Tags { get; set; }
    }
}
