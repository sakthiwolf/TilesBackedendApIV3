using Microsoft.EntityFrameworkCore;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositoryContracts;
using Tiles.Infrastructure.Data;

namespace Tiles.Infrastructure.Repositories
{
    public class SellerRepository : ISellerRepository 
    {
        private readonly AppDbContext _context;

        public SellerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seller>> GetAllAsync(string search, int pageNo, int rowsPerPage)
        {
            var query = _context.Sellers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s =>
                    s.Name.Contains(search) ||
                    s.DealerName.Contains(search) ||
                    s.State.Contains(search) ||
                    s.City.Contains(search) ||
                    s.Address.Contains(search) ||
                    s.SerialNumber.ToString() == search
                );
            }

            return await query
                .OrderBy(s => s.SerialNumber)
                .Skip((pageNo - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string search)
        {
            var query = _context.Sellers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s =>
                    s.Name.Contains(search) ||
                    s.DealerName.Contains(search) ||
                    s.State.Contains(search) ||
                    s.City.Contains(search) ||
                    s.Address.Contains(search) ||
                    s.SerialNumber.ToString() == search
                );
            }

            return await query.CountAsync();
        }

        public async Task<Seller?> GetByIdAsync(Guid id) => await _context.Sellers.FindAsync(id);

        public async Task<Seller?> GetByEmailAsync(string email)
        {
            return await _context.Sellers.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<int> GetNextSerialNumberAsync()
        {
            var max = await _context.Sellers.MaxAsync(s => (int?)s.SerialNumber);
            return (max ?? 0) + 1;
        }

        public async Task AddAsync(Seller seller)
        {
            await _context.Sellers.AddAsync(seller);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            _context.Sellers.Update(seller);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Seller seller)
        {
            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
        }
    }
}
