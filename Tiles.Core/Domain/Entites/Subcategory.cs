using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tiles.Core.Domain.Entites;

namespace Tiles.Core.Domain.Entities
{
    public class Subcategory
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}
