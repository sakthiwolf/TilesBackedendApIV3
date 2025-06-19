using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositoryContracts;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Core.DTO.EcatalogCategory;

using Tiles.Core.ServiceContracts;

namespace Tiles.Core.Services
{
    public class EcatalogCategoryService : IEcatalogCategoryService
    {
        private readonly IEcatalogCategoryRepository _repo;

        public EcatalogCategoryService(IEcatalogCategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<EcatalogCategoryResponse> CreateAsync(CreateEcatalogCategoryRequest request)
        {
            var category = new EcatalogCategory
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CoverPhoto = request.CoverPhoto
            };

            var created = await _repo.CreateAsync(category);
            return MapToResponse(created);
        }

        public async Task<IEnumerable<EcatalogCategoryResponse>> GetAllAsync()
        {
            var categories = await _repo.GetAllAsync();
            return categories.Select(MapToResponse);
        }

        public async Task<EcatalogCategoryResponse?> GetByIdAsync(Guid id)
        {
            var category = await _repo.GetByIdAsync(id);
            return category == null ? null : MapToResponse(category);
        }

        public async Task<EcatalogCategoryResponse?> UpdateAsync(Guid id, UpdateEcatalogCategoryRequest request)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return null;

            category.Name = request.Name;
            category.CoverPhoto = request.CoverPhoto;

            var updated = await _repo.UpdateAsync(category);
            return MapToResponse(updated!);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }

        private EcatalogCategoryResponse MapToResponse(EcatalogCategory c)
        {
            return new EcatalogCategoryResponse
            {
                Id = c.Id,
                Name = c.Name,
                CoverPhoto = c.CoverPhoto
            };
        }
    }
}
