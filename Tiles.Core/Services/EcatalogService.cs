using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Core.DTO.Ecatalog;
using Tiles.Core.ServiceContracts;

namespace Tiles.Core.Services
{
    public class EcatalogService : IEcatalogService
    {
        private readonly IEcatalogRepository _repo;
        private readonly IEcatalogCategoryRepository _categoryRepo;

        public EcatalogService(IEcatalogRepository repo, IEcatalogCategoryRepository categoryRepo)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
        }

        public async Task<EcatalogResponse> CreateAsync(EcatalogRequest request)
        {
            var ecatalog = new Ecatalog
            {
                Id = Guid.NewGuid(),
                CategoryId = request.CategoryId,
                CatalogName = request.CatalogName,
                CoverPhoto = request.CoverPhoto,
                FileUrl = request.FileUrl,
                IsActive = request.IsActive,
                Tags = request.Tags
            };

            await _repo.CreateAsync(ecatalog);
            var category = await _categoryRepo.GetByIdAsync(request.CategoryId);

            return new EcatalogResponse
            {
                Id = ecatalog.Id,
                CategoryId = ecatalog.CategoryId,
                CategoryName = category?.Name ?? "",
                CatalogName = ecatalog.CatalogName,
                CoverPhoto = ecatalog.CoverPhoto,
                FileUrl = ecatalog.FileUrl,
                IsActive = ecatalog.IsActive,
                Tags = ecatalog.Tags
            };
        }

        public async Task<IEnumerable<EcatalogResponse>> GetAllAsync(Guid? categoryId)
        {
            var ecatalogs = await _repo.GetAllAsync(categoryId);
            return ecatalogs.Select(e => new EcatalogResponse
            {
                Id = e.Id,
                CategoryId = e.CategoryId,
                CategoryName = e.Category?.Name ?? "",
                CatalogName = e.CatalogName,
                CoverPhoto = e.CoverPhoto,
                FileUrl = e.FileUrl,
                IsActive = e.IsActive,
                Tags = e.Tags
            });
        }

        public async Task<EcatalogResponse?> UpdateAsync(Guid id, EcatalogRequest request)
        {
            var ecatalog = new Ecatalog
            {
                Id = id,
                CategoryId = request.CategoryId,
                CatalogName = request.CatalogName,
                CoverPhoto = request.CoverPhoto,
                FileUrl = request.FileUrl,
                IsActive = request.IsActive,
                Tags = request.Tags
            };

            var updated = await _repo.UpdateAsync(ecatalog);
            if (updated == null) return null;

            var category = await _categoryRepo.GetByIdAsync(updated.CategoryId);

            return new EcatalogResponse
            {
                Id = updated.Id,
                CategoryId = updated.CategoryId,
                CategoryName = category?.Name ?? "",
                CatalogName = updated.CatalogName,
                CoverPhoto = updated.CoverPhoto,
                FileUrl = updated.FileUrl,
                IsActive = updated.IsActive,
                Tags = updated.Tags
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
