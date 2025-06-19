using Tiles.Core.Domain.Entites;

namespace Tiles.Core.Domain.RepositroyContracts
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllWithSubcategoriesAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category?> GetByNameAsync(string name);
        Task<Category> CreateAsync(Category category);
        Task<Category?> UpdateAsync(Category category);
        Task<bool> DeleteAsync(Guid id);
    }
}
