using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entites;

namespace Tiles.Core.Domain.RepositroyContracts
{
    public interface IEcatalogCategoryRepository
    {
        Task<EcatalogCategory> CreateAsync(EcatalogCategory category);
        Task<IEnumerable<EcatalogCategory>> GetAllAsync();
        Task<EcatalogCategory?> GetByIdAsync(Guid id);
        Task<EcatalogCategory?> UpdateAsync(EcatalogCategory category);
        Task<bool> DeleteAsync(Guid id);
    }
}
