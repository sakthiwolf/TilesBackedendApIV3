using Tiles.Core.DTO.Categorys;

namespace Tiles.Core.ServiceContracts
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllWithSubcategoriesAsync();
        Task<CategoryResponse?> GetByIdAsync(Guid id);
        Task<CategoryResponse> CreateAsync(CreateCategoryRequest request);
        Task<CategoryResponse?> UpdateAsync(Guid id, UpdateCategoryRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
