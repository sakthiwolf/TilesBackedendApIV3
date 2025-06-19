
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.Entities;
namespace Tiles.Core.Domain.Entites
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string SerialNumber { get; set; } = null!;

        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;

        [Required]
        public Guid SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public Subcategory SubCategory { get; set; } = null!;

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
