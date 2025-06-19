using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entites;

namespace Tiles.Core.Domain.RepositroyContracts
{
    public interface IInstagramRepository
    {
        Task<Instagram> CreateAsync(Instagram instagram);
        Task<IEnumerable<Instagram>> GetAllAsync();
        Task<Instagram?> UpdateAsync(Instagram instagram);
        Task<bool> DeleteAsync(Guid id);
        Task<Instagram?> GetByIdAsync(Guid id);
    }
}
