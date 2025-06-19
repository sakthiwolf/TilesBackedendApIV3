using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.DTO.EcatalogCategory;

namespace Tiles.Core.ServiceContracts
{
    public interface IEcatalogCategoryService
    {
        Task<EcatalogCategoryResponse> CreateAsync(CreateEcatalogCategoryRequest request);
        Task<IEnumerable<EcatalogCategoryResponse>> GetAllAsync();
        Task<EcatalogCategoryResponse?> GetByIdAsync(Guid id);
        Task<EcatalogCategoryResponse?> UpdateAsync(Guid id, UpdateEcatalogCategoryRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
