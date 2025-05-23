using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entities;

namespace Tiles.Core.Domain.RepositroyContracts
{
    public interface ISubcategoryRepository
    {
        Task<Subcategory> CreateAsync(Subcategory subcategory);
        Task<IEnumerable<Subcategory>> GetByCategoryIdAsync(Guid categoryId);
    }
}
