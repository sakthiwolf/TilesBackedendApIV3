using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.DTO.EcatalogCategory
{
    public class CreateEcatalogCategoryRequest
    {
        public string Name { get; set; } = null!;
        public string CoverPhoto { get; set; } = null!;
    }
}
