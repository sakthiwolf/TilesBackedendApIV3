using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tiles.Core.DTO.ProductDto
{
    public class ProductRequest
    {
        [Required(ErrorMessage = "Category is required.")]
        public Guid Category { get; set; }

        [Required(ErrorMessage = "SubCategory is required.")]
        public Guid SubCategory { get; set; }

        [Required(ErrorMessage = "ProductName is required.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 150 characters.")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ProductImage is required.")]
        public string ProductImage { get; set; } = string.Empty;

        [Required(ErrorMessage = "ProductSizes are required.")]
        [MinLength(1, ErrorMessage = "At least one product size must be specified.")]
        public List<string> ProductSizes { get; set; } = new List<string>();

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Colors are required.")]
        [MinLength(1, ErrorMessage = "At least one color must be specified.")]
        public List<string> Colors { get; set; } = new List<string>();

        public string Disclaimer { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Stock must be zero or more.")]
        public int Stock { get; set; }
    }
}
