using Microsoft.EntityFrameworkCore;
using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositoryContracts;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Infrastructure.Data;

namespace Tiles.Infrastructure.Repositories
{
    public class EcatalogCategoryRepository : IEcatalogCategoryRepository
    {
        private readonly AppDbContext _context;

        public EcatalogCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EcatalogCategory> CreateAsync(EcatalogCategory category)
        {
            _context.EcatalogCategories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<EcatalogCategory>> GetAllAsync()
        {
            return await _context.EcatalogCategories.ToListAsync();
        }

        public async Task<EcatalogCategory?> GetByIdAsync(Guid id)
        {
            return await _context.EcatalogCategories.FindAsync(id);
        }

        public async Task<EcatalogCategory?> UpdateAsync(EcatalogCategory category)
        {
            _context.EcatalogCategories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _context.EcatalogCategories.FindAsync(id);
            if (category == null) return false;

            _context.EcatalogCategories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
