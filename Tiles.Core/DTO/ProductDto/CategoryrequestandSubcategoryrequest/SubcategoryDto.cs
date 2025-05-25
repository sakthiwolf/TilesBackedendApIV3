using System;
using System.ComponentModel.DataAnnotations;

namespace Tiles.Core.DTO.ProductDto.CategoryrequestandSubcategoryrequest
{
    public class SubcategoryRequest
    {
        [Required(ErrorMessage = "Subcategory name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Subcategory name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "CategoryId is required.")]
        public Guid CategoryId { get; set; }
    }
}
