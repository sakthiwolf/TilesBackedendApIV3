using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.DTO.ProductDto.CategoryrequestandSubcategoryrequest
{
    public class SubcategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
}
