using System;
using System.ComponentModel.DataAnnotations;

namespace Tiles.Core.Domain.Entities
{
    public class Seller
    {
        [Key]
        public Guid Id { get; set; }

  
        public int SerialNumber { get; set; }

        public string Name { get; set; } = null!;

     
        public string Map { get; set; } = null!;

    
        public string WhatsappNumber { get; set; } = null!;

        public string Email { get; set; } = null!;


        public string DealerName { get; set; } = null!;

     
        public string State { get; set; } = null!;

      
        public string City { get; set; } = null!;

      
        public string Address { get; set; } = null!;

      
        public DateTime CreatedAt { get; set; }

    
        public DateTime UpdatedAt { get; set; }
    }
}
