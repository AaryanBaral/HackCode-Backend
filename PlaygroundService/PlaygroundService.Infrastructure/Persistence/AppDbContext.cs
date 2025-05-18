using Microsoft.EntityFrameworkCore;
using PlaygroundService.Domain.Entities;

namespace PlaygroundService.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Playground> Playgrounds { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Playground>(entity =>
            {

                entity.HasKey(q => q.Id);

                entity.Property(q => q.Id)
                .IsRequired()
                .HasMaxLength(36);

                entity.Property(q => q.IsDeleted)
                .HasDefaultValue(false);

                entity.Property(q => q.IsSuccess)
                .HasDefaultValue(false);


                entity.Property(q => q.UserId)
                .IsRequired()
                .HasMaxLength(36);

                entity.Property(q => q.Language)
                .IsRequired()
                .HasMaxLength(255);

                entity.Property(q => q.Output)
                .IsRequired();

                entity.Property(q => q.Error)
                .IsRequired();

                entity.Property(q => q.Code)
                .IsRequired();


                entity.HasQueryFilter(q => !q.IsDeleted);

                entity.HasIndex(q => q.CreatedAt);
                entity.HasIndex(q => q.IsDeleted);


            });
        }
    }

}