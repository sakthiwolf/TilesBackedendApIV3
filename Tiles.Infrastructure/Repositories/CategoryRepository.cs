using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Infrastructure.Data;

namespace Tiles.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing Category entities in the database.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor to inject the application's DbContext.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public CategoryRepository(AppDbContext context) => _context = context;

        /// <summary>
        /// Adds a new Category to the database.
        /// </summary>
        /// <param name="category">The Category entity to be created.</param>
        /// <returns>The created Category entity.</returns>
        public async Task<Category> CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        /// <summary>
        /// Retrieves all Category records from the database.
        /// </summary>
        /// <returns>A list of all Category entities.</returns>
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
