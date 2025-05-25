using System.ComponentModel.DataAnnotations;

namespace Tiles.Core.Domain.Entities
{
    public class Category
    {
        
        public Guid Id { get; set; }

     
        public string Name { get; set; } = string.Empty;

       
        public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
    }
}
