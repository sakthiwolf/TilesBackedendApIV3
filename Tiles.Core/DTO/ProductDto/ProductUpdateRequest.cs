using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tiles.Core.DTO.ProductDto
{
    public class ProductUpdateRequest
    {
        public Guid? Category { get; set; }

        public Guid? SubCategory { get; set; }

        [StringLength(150, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 150 characters.")]
        public string? ProductName { get; set; }

        public string? ProductImage { get; set; }

        [MinLength(1, ErrorMessage = "At least one product size must be specified.")]
        public List<string>? ProductSizes { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        [MinLength(1, ErrorMessage = "At least one color must be specified.")]
        public List<string>? Colors { get; set; }

        public string? Disclaimer { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock must be zero or more.")]
        public int? Stock { get; set; }
    }
}
