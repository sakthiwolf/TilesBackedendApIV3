using System;

namespace Tiles.Core.DTO.Categorys
{
    public class UpdateSubcategoryRequest
    {
        public string Name { get; set; } = null!;
        public Guid CategoryId { get; set; }
    }
}
