using ConstrainService.Application.Interfaces.Repository;
using ConstrainService.Domain.Entities;
using ConstrainService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConstrainService.Infrastructure.Repository
{
    public class ConstrainRepository(AppDbContext context) : IConstrainRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddConstrain(Constrain constrain)
        {
            await _context.Constrains.AddAsync(constrain);
            await _context.SaveChangesAsync();
        }

        public async Task<Constrain> GetConstrainById(string id)
        {
            return await _context.Constrains.FindAsync(id) ?? throw new KeyNotFoundException("constrain of given id not found");
        }
        public async Task<List<Constrain>> GetAllConstrain()
        {
            var constrains = await _context.Constrains.ToListAsync();
            if (constrains.Count == 0) throw new NullReferenceException(nameof(constrains));
            return constrains;
        }

        public async Task SoftDelete(string id)
        {
            var constrain = await GetConstrainById(id);
            constrain.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        public async Task DeletePermanently(string id)
        {
            var constrain = await _context.Constrains.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.ConstrainId == id && c.IsDeleted)
                ?? throw new KeyNotFoundException("constrain of given id not found");
            _context.Constrains.Remove(constrain);
            await _context.SaveChangesAsync();
        }
    }
}