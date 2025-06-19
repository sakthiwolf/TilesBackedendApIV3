using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tiles.Core.DTO.Categorys
{
    public class CreateSubcategoryRequest
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
    }
}

