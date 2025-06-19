using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entites;

namespace Tiles.Core.Domain.RepositroyContracts
{

    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync(Guid? categoryId, Guid? subCategoryId, int page, int limit);
        Task<int> CountAsync(Guid? categoryId, Guid? subCategoryId);
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product?> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);

    }
}
