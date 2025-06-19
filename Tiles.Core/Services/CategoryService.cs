using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Core.DTO.Categorys;
using Tiles.Core.ServiceContracts;

namespace Tiles.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CategoryResponse>> GetAllWithSubcategoriesAsync()
        {
            var categories = await _repo.GetAllWithSubcategoriesAsync();
            return categories.Select(MapToResponse).ToList();
        }

        public async Task<CategoryResponse?> GetByIdAsync(Guid id)
        {
            var category = await _repo.GetByIdAsync(id);
            return category == null ? null : MapToResponse(category);
        }

        public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request)
        {
            var existing = await _repo.GetByNameAsync(request.Name);
            if (existing != null)
                throw new ApplicationException($"Category '{request.Name}' already exists.");

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            var created = await _repo.CreateAsync(category);
            return MapToResponse(created);
        }

        public async Task<CategoryResponse?> UpdateAsync(Guid id, UpdateCategoryRequest request)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            var duplicate = await _repo.GetByNameAsync(request.Name);
            if (duplicate != null && duplicate.Id != id)
                throw new ApplicationException($"Category '{request.Name}' already exists.");

            existing.Name = request.Name;

            var updated = await _repo.UpdateAsync(existing);
            return updated == null ? null : MapToResponse(updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }

        private CategoryResponse MapToResponse(Category c)
        {
            return new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name,
                Subcategories = c.Subcategories.Select(sc => new SubcategoryResponse
                {
                    Id = sc.Id,
                    Name = sc.Name
                }).ToList()
            };
        }
    }
}
