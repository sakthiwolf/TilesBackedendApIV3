using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.DTO.EcatalogCategory
{
    public class EcatalogCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string CoverPhoto { get; set; } = null!;
    }
}
