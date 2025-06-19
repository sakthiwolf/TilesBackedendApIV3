using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Core.DTO.Categorys;
using Tiles.Core.ServiceContracts;

namespace Tiles.Core.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _repo;

        public SubcategoryService(ISubcategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<SubcategoryResponse>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(MapToResponse).ToList();
        }

        public async Task<SubcategoryResponse?> GetByIdAsync(Guid id)
        {
            var sub = await _repo.GetByIdAsync(id);
            return sub is null ? null : MapToResponse(sub);
        }

        public async Task<SubcategoryResponse> CreateAsync(CreateSubcategoryRequest request)
        {
            var sub = new Subcategory
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CategoryId = request.CategoryId
            };

            var created = await _repo.CreateAsync(sub);
            return MapToResponse(created);
        }

        public async Task<SubcategoryResponse?> UpdateAsync(Guid id, UpdateSubcategoryRequest request)
        {
            var sub = await _repo.GetByIdAsync(id);
            if (sub == null) return null;

            sub.Name = request.Name;
            sub.CategoryId = request.CategoryId;

            var updated = await _repo.UpdateAsync(sub);
            return MapToResponse(updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }

        private SubcategoryResponse MapToResponse(Subcategory sub)
        {
            return new SubcategoryResponse
            {
                Id = sub.Id,
                Name = sub.Name,
                //CategoryId = sub.CategoryId
            };
        }
    }
}
