using Tiles.Core.DTO.SellerDto;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositoryContracts;
using Tiles.Core.ServiceContracts;

namespace Tiles.Core.Services
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepository _repo;

        public SellerService(ISellerRepository repo)
        {
            _repo = repo;
        }

        public async Task<(IEnumerable<SellerResponseDto>, int)> GetAllAsync(string search, int pageNo, int rowsPerPage)
        {
            var sellers = await _repo.GetAllAsync(search, pageNo, rowsPerPage);
            var total = await _repo.GetTotalCountAsync(search);

            var result = sellers.Select(s => new SellerResponseDto
            {
                _id = s.Id,
                SerialNumber = s.SerialNumber,
                Name = s.Name, 
                Map = s.Map,
                WhatsappNumber = s.WhatsappNumber,
                Email = s.Email,
                DealerName = s.DealerName,
                State = s.State,
                City = s.City,
                Address = s.Address
            });

            return (result, total);
        }

        public async Task<SellerResponseDto> GetByIdAsync(Guid id)
        {
            var seller = await _repo.GetByIdAsync(id);
            if (seller == null) throw new Exception("Seller not found");

            return new SellerResponseDto
            {
                _id = seller.Id,
                SerialNumber = seller.SerialNumber,
                Name = seller.Name,
                Map = seller.Map,
                WhatsappNumber = seller.WhatsappNumber,
                Email = seller.Email,
                DealerName = seller.DealerName,
                State = seller.State,
                City = seller.City,
                Address = seller.Address
            };
        }

        public async Task<SellerResponseDto> CreateAsync(SellerRequestDto dto)
        {
            if (await _repo.GetByEmailAsync(dto.Email) != null)
                throw new Exception("Email already exists");

            var seller = new Seller
            {
                Id = Guid.NewGuid(),
                SerialNumber = await _repo.GetNextSerialNumberAsync(),
                Name = dto.Name,
                Map = dto.Map,
                WhatsappNumber = dto.WhatsappNumber,
                Email = dto.Email,
                DealerName = dto.DealerName,
                State = dto.State,
                City = dto.City,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(seller);
            return await GetByIdAsync(seller.Id);
        }

        public async Task<SellerResponseDto> UpdateAsync(Guid id, SellerRequestDto dto)
        {
            var seller = await _repo.GetByIdAsync(id);
            if (seller == null) throw new Exception("Seller not found");

            seller.Name = dto.Name ?? seller.Name;
            seller.Map = dto.Map ?? seller.Map;
            seller.WhatsappNumber = dto.WhatsappNumber ?? seller.WhatsappNumber;
            seller.Email = dto.Email ?? seller.Email;
            seller.DealerName = dto.DealerName ?? seller.DealerName;
            seller.State = dto.State ?? seller.State;
            seller.City = dto.City ?? seller.City;
            seller.Address = dto.Address ?? seller.Address;
            seller.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(seller);
            return await GetByIdAsync(seller.Id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var seller = await _repo.GetByIdAsync(id);
            if (seller == null) throw new Exception("Seller not found");
            await _repo.DeleteAsync(seller);
        }
    }
}
