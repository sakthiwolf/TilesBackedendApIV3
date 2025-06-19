using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tiles.Core.DTO.Categorys
{
    public class SubcategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public Guid CategoryId { get; set; } // Added this property to fix the error  
    }




}
