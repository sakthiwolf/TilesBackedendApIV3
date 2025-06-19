using Microsoft.EntityFrameworkCore;
using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositoryContracts;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Infrastructure.Data;

namespace Tiles.Infrastructure.Repositories
{
    public class InstagramRepository : IInstagramRepository
    {
        private readonly AppDbContext _context;

        public InstagramRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Instagram> CreateAsync(Instagram instagram)
        {
            _context.Instagrams.Add(instagram);
            await _context.SaveChangesAsync();
            return instagram;
        }

        public async Task<IEnumerable<Instagram>> GetAllAsync()
        {
            return await _context.Instagrams.ToListAsync();
        }

        public async Task<Instagram?> GetByIdAsync(Guid id)
        {
            return await _context.Instagrams.FindAsync(id);
        }

        public async Task<Instagram?> UpdateAsync(Instagram instagram)
        {
            var existing = await _context.Instagrams.FindAsync(instagram.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(instagram);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var instagram = await _context.Instagrams.FindAsync(id);
            if (instagram == null) return false;

            _context.Instagrams.Remove(instagram);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
