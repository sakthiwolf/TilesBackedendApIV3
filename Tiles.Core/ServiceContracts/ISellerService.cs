using Tiles.Core.DTO.SellerDto;

namespace Tiles.Core.ServiceContracts
{
    public interface ISellerService
    {
        Task<(IEnumerable<SellerResponseDto>, int)> GetAllAsync(string search, int pageNo, int rowsPerPage);
        Task<SellerResponseDto> GetByIdAsync(Guid id);
        Task<SellerResponseDto> CreateAsync(SellerRequestDto dto);
        Task<SellerResponseDto> UpdateAsync(Guid id, SellerRequestDto dto);
        Task DeleteAsync(Guid id);
    }
}
