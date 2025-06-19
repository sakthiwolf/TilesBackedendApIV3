using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.DTO.ProductDto
{
    public class ProductRequest
    {
        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid SubCategoryId { get; set; }

        [Required]
        public string ProductName { get; set; } = null!;

        [Required]
        public string ProductImage { get; set; } = null!;

        [Required]
        public List<string> ProductSizes { get; set; } = new();

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public List<string> Colors { get; set; } = new();

        [Required]
        public string Disclaimer { get; set; } = null!;

        [Required]
        public int Stock { get; set; }

        public string? Link360 { get; set; }
    }
}
