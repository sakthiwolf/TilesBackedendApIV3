using Microsoft.EntityFrameworkCore;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Infrastructure.Data;

namespace Tiles.Infrastructure.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly AppDbContext _context;

        public SubcategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Subcategory> CreateAsync(Subcategory subcategory)
        {
            // Log the CategoryId being checked (optional, requires ILogger)
            // _logger.LogInformation($"Checking existence of CategoryId: {subcategory.CategoryId}");

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == subcategory.CategoryId);
            if (!categoryExists)
            {
                // Optionally, log the error
                // _logger.LogWarning($"CategoryId {subcategory.CategoryId} does not exist.");
                throw new InvalidOperationException($"Invalid CategoryId '{subcategory.CategoryId}'. The referenced category does not exist.");
            }

            _context.Subcategories.Add(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }


        public async Task<Subcategory?> GetByIdAsync(Guid id)
        {
            return await _context.Subcategories.FindAsync(id);
        }

        public async Task<IEnumerable<Subcategory>> GetAllAsync()
        {
            return await _context.Subcategories.ToListAsync();
        }

        public async Task<Subcategory?> UpdateAsync(Subcategory subcategory)
        {
            _context.Subcategories.Update(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var sub = await _context.Subcategories.FindAsync(id);
            if (sub == null) return false;
            _context.Subcategories.Remove(sub);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> CategoryExistsAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
