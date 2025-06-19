using Tiles.Core.DTO.Categorys;

namespace Tiles.Core.ServiceContracts
{
    public interface ISubcategoryService
    {
        Task<List<SubcategoryResponse>> GetAllAsync();
        Task<SubcategoryResponse?> GetByIdAsync(Guid id);
        Task<SubcategoryResponse> CreateAsync(CreateSubcategoryRequest request);
        Task<SubcategoryResponse?> UpdateAsync(Guid id, UpdateSubcategoryRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
