using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entites;

namespace Tiles.Core.Domain.RepositroyContracts
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);  // Keep only one method to fetch by email
        Task<User?> GetByIdAsync(Guid id);
        Task<List<User>> GetUsersAsync(string search, int pageNo, int rowsPerPage);
        Task<int> GetTotalCountAsync(string search);
        Task<int> GetNextSerialNumberAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }

}
