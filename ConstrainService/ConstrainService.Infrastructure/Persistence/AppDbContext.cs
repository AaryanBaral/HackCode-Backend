using ConstrainService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConstrainService.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Constrain> Constrains { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Constrain>(entity =>
            {
            });

        }
    }
}