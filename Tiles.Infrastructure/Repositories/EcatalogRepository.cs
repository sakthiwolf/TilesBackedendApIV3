using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Infrastructure.Data;

namespace Tiles.Infrastructure.Repositories
{
    public class EcatalogRepository : IEcatalogRepository
    {
        private readonly AppDbContext _context;

        public EcatalogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Ecatalog> CreateAsync(Ecatalog ecatalog)
        {
            _context.Ecatalogs.Add(ecatalog);
            await _context.SaveChangesAsync();
            return ecatalog;
        }

        public async Task<IEnumerable<Ecatalog>> GetAllAsync(Guid? categoryId = null)
        {
            var query = _context.Ecatalogs.Include(e => e.Category).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(e => e.CategoryId == categoryId);

            return await query.ToListAsync();
        }

        public async Task<Ecatalog?> GetByIdAsync(Guid id)
        {
            return await _context.Ecatalogs.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Ecatalog?> UpdateAsync(Ecatalog ecatalog)
        {
            var existing = await _context.Ecatalogs.FindAsync(ecatalog.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(ecatalog);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var ecatalog = await _context.Ecatalogs.FindAsync(id);
            if (ecatalog == null) return false;

            _context.Ecatalogs.Remove(ecatalog);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
