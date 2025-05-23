using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.DTO.ProductDto
{
    public class ProductUpdateRequest
    {
        public Guid? Category { get; set; }
        public Guid? SubCategory { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public List<string>? ProductSizes { get; set; }
        public string? Description { get; set; }
        public List<string>? Colors { get; set; }
        public string? Disclaimer { get; set; }
        public int? Stock { get; set; }
    }
}
