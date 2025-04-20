using Microsoft.EntityFrameworkCore;
using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Infrastructure.data;

namespace Tiles.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) => _context = context;

        // Use only GetByEmailAsync method
        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByIdAsync(Guid id) =>
            await _context.Users.FindAsync(id);

        public async Task<List<User>> GetUsersAsync(string search, int pageNo, int rowsPerPage) =>
            await _context.Users
                .Where(u => u.Name.Contains(search) || u.Email.Contains(search) || u.Phone.Contains(search))
                .Skip((pageNo - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToListAsync();

        public async Task<int> GetTotalCountAsync(string search) =>
            await _context.Users
                .CountAsync(u => u.Name.Contains(search) || u.Email.Contains(search) || u.Phone.Contains(search));

        public async Task<int> GetNextSerialNumberAsync()
        {
            var max = await _context.Users.MaxAsync(u => (int?)u.SerialNumber);
            return (max ?? 0) + 1;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
