using Tiles.Core.Domain.Entities;

namespace Tiles.Core.Domain.RepositroyContracts
{
    public interface ISubcategoryRepository
    {
        Task<Subcategory> CreateAsync(Subcategory subcategory);
        Task<Subcategory?> GetByIdAsync(Guid id);
        Task<IEnumerable<Subcategory>> GetAllAsync();
        Task<Subcategory?> UpdateAsync(Subcategory subcategory);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> CategoryExistsAsync(Guid categoryId);
    }
}
