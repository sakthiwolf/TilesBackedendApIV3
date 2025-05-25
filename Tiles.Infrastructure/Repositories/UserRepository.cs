using Microsoft.EntityFrameworkCore;

using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Infrastructure.Data;

namespace Tiles.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing User entities.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        public async Task<User?> GetByIdAsync(Guid id) =>
            await _context.Users.FindAsync(id);

        /// <summary>
        /// Retrieves a paginated and filtered list of users.
        /// </summary>
        /// <param name="search">Search keyword to filter by name, email, or phone.</param>
        /// <param name="pageNo">The current page number.</param>
        /// <param name="rowsPerPage">Number of users per page.</param>
        /// <returns>A list of users matching the filter and pagination criteria.</returns>
        public async Task<List<User>> GetUsersAsync(string search, int pageNo, int rowsPerPage) =>
            await _context.Users
                .Where(u =>
                    u.Name.Contains(search) ||
                    u.Email.Contains(search) ||
                    u.Phone.Contains(search))
                .Skip((pageNo - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToListAsync();

        /// <summary>
        /// Gets the total number of users that match a search keyword.
        /// </summary>
        /// <param name="search">Search keyword to filter by name, email, or phone.</param>
        /// <returns>The total count of users matching the filter.</returns>
        public async Task<int> GetTotalCountAsync(string search) =>
            await _context.Users.CountAsync(u =>
                u.Name.Contains(search) ||
                u.Email.Contains(search) ||
                u.Phone.Contains(search));

        /// <summary>
        /// Retrieves the next available serial number for a new user.
        /// </summary>
        /// <returns>The next sequential serial number.</returns>
        public async Task<int> GetNextSerialNumberAsync()
        {
            var max = await _context.Users.MaxAsync(u => (int?)u.SerialNumber);
            return (max ?? 0) + 1;
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        public async Task AddAsync(User user)
        {
            user.OtpExpiry = DateTime.UtcNow.AddMinutes(5); // Always use UTC
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="user">The user entity with updated values.</param>
        public async Task UpdateAsync(User user)
        {
            user.OtpExpiry = user.OtpExpiry?.ToUniversalTime(); // Ensure UTC time
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="user">The user entity to delete.</param>
        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
