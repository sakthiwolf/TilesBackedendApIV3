using Tiles.Core.Domain.Entities;

namespace Tiles.Core.Domain.RepositoryContracts
{
    public interface ISellerRepository
    {
        Task<IEnumerable<Seller>> GetAllAsync(string search, int pageNo, int rowsPerPage);
        Task<int> GetTotalCountAsync(string search);
        Task<Seller?> GetByIdAsync(Guid id);
        Task<Seller?> GetByEmailAsync(string email);
        Task<int> GetNextSerialNumberAsync();
        Task AddAsync(Seller seller);
        Task UpdateAsync(Seller seller);
        Task DeleteAsync(Seller seller);
    }
}
