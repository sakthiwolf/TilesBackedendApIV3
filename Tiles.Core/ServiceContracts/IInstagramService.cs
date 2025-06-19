using Tiles.Core.DTO.Instagram;


namespace Tiles.Core.ServiceContracts
{
    public interface IInstagramService
    {
        Task<InstagramResponse> CreateAsync(InstagramRequest request);
        Task<IEnumerable<InstagramResponse>> GetAllAsync();
        Task<InstagramResponse?> UpdateAsync(Guid id, InstagramRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
