using ConstrainService.Application.Interfaces.Repository;
using ConstrainService.Domain.Entities;
using ConstrainService.Infrastructure.Persistence;

namespace ConstrainService.Infrastructure.Repository
{
    public class ConstrainService(AppDbContext context) : IConstrainRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddConstrain(Constrain constrain)
        {
            await _context.Constrains.AddAsync(constrain);
            await _context.SaveChangesAsync();
        }
    }
}