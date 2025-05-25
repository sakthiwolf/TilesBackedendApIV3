using System.ComponentModel.DataAnnotations;

namespace Tiles.Core.DTO.ProductDto.CategoryrequestandSubcategoryrequest
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}
