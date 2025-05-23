using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Infrastructure.Data;

namespace Tiles.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing Subcategory entities.
    /// </summary>
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubcategoryRepository"/> class with the specified DbContext.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public SubcategoryRepository(AppDbContext context) => _context = context;

        /// <summary>
        /// Creates a new Subcategory in the database after validating its Category exists.
        /// </summary>
        /// <param name="subcategory">The Subcategory entity to create.</param>
        /// <returns>The created Subcategory entity.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the associated Category does not exist.</exception>
        public async Task<Subcategory> CreateAsync(Subcategory subcategory)
        {
            // Ensure the referenced Category exists before adding the Subcategory
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == subcategory.CategoryId);
            if (!categoryExists)
                throw new InvalidOperationException($"Category with Id '{subcategory.CategoryId}' does not exist.");

            _context.Subcategories.Add(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }

        /// <summary>
        /// Retrieves all Subcategories for a given Category ID.
        /// </summary>
        /// <param name="categoryId">The ID of the Category to filter Subcategories by.</param>
        /// <returns>A list of Subcategories belonging to the specified Category.</returns>
        public async Task<IEnumerable<Subcategory>> GetByCategoryIdAsync(Guid categoryId)
        {
            return await _context.Subcategories
                .Where(s => s.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
