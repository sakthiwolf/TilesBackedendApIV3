using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entities;
using Tiles.Core.DTO.ProductDto.CategoryrequestandSubcategoryrequest;

namespace Tiles.Core.ServiceContracts
{
    public interface ISubcategoryService
    {
        Task<Subcategory> CreateSubcategoryAsync(SubcategoryRequest request);
        Task<IEnumerable<Subcategory>> GetSubcategoriesByCategoryIdAsync(Guid categoryId);
    }
}
