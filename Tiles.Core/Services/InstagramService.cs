using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositoryContracts;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Core.DTO.Instagram;

using Tiles.Core.ServiceContracts;

namespace Tiles.Core.Services
{
    public class InstagramService : IInstagramService
    {
        private readonly IInstagramRepository _repo;

        public InstagramService(IInstagramRepository repo)
        {
            _repo = repo;
        }

        public async Task<InstagramResponse> CreateAsync(InstagramRequest request)
        {
            var insta = new Instagram
            {
                Id = Guid.NewGuid(),
                File = request.File,
                CoverPhoto = request.CoverPhoto
            };

            await _repo.CreateAsync(insta);

            return new InstagramResponse
            {
                Id = insta.Id,
                File = insta.File,
                CoverPhoto = insta.CoverPhoto
            };
        }

        public async Task<IEnumerable<InstagramResponse>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(i => new InstagramResponse
            {
                Id = i.Id,
                File = i.File,
                CoverPhoto = i.CoverPhoto
            });
        }

        public async Task<InstagramResponse?> UpdateAsync(Guid id, InstagramRequest request)
        {
            var insta = new Instagram
            {
                Id = id,
                File = request.File,
                CoverPhoto = request.CoverPhoto
            };

            var updated = await _repo.UpdateAsync(insta);
            if (updated == null) return null;

            return new InstagramResponse
            {
                Id = updated.Id,
                File = updated.File,
                CoverPhoto = updated.CoverPhoto
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
