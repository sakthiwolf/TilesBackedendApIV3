using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiles.Core.Domain.Entities
{
    public class Subcategory
    {
        [Key]
        public Guid Id { get; set; }

      
        public string Name { get; set; } = string.Empty;

       
        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
    }
}
