using Tiles.Core.Domain.Entites;

namespace Tiles.Core.Domain.RepositroyContracts
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByFilterAsync(Guid? categoryId, Guid? subCategoryId);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
    }
}
