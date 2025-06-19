using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entites;

namespace Tiles.Core.Domain.RepositroyContracts
{
    public interface IEcatalogRepository
    {
        Task<Ecatalog> CreateAsync(Ecatalog ecatalog);
        Task<IEnumerable<Ecatalog>> GetAllAsync(Guid? categoryId = null);
        Task<Ecatalog?> GetByIdAsync(Guid id);
        Task<Ecatalog?> UpdateAsync(Ecatalog ecatalog);
        Task<bool> DeleteAsync(Guid id);
    }
}
