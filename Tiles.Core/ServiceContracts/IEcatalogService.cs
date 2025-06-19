using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.DTO.Ecatalog;

namespace Tiles.Core.ServiceContracts
{
    public interface IEcatalogService
    {
        Task<EcatalogResponse> CreateAsync(EcatalogRequest request);
        Task<IEnumerable<EcatalogResponse>> GetAllAsync(Guid? categoryId);
        Task<EcatalogResponse?> UpdateAsync(Guid id, EcatalogRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
