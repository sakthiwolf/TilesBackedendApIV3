using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tiles.Core.Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }  // PostgreSQL UUID support

   
        public Guid Category { get; set; }

    
        public Guid SubCategory { get; set; }

        
        public string ProductName { get; set; } = string.Empty;

       
        public string? ProductImage { get; set; }

       
        public List<string> ProductSizes { get; set; } = new List<string>();

        public string? Description { get; set; }

        public List<string>? Colors { get; set; } = new List<string>();

   
        public string? Disclaimer { get; set; }

        public int Stock { get; set; }
    }
}
