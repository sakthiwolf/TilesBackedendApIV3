using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entities;

namespace Tiles.Core.Domain.Entites
{

    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
    }
}
