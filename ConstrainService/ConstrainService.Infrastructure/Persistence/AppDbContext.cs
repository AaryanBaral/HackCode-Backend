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
                entity.HasKey(c => c.ConstrainId);

                entity.Property(c => c.ConstrainId)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(c => c.QuestionId)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(c => c.InputDescription)
                    .IsRequired()
                    .HasColumnType("TEXT");

                entity.Property(c => c.OutputDescription)
                    .IsRequired()
                    .HasColumnType("TEXT");

                entity.Property(c => c.AdditionalNotes)
                    .IsRequired()
                    .HasColumnType("TEXT");

                entity.Property(c => c.TimeLimit)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(c => c.MemoryLimit)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(c => c.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(c => c.CreatedAt)
                    .IsRequired();

                entity.Property(c => c.UpdatedAt)
                    .IsRequired();

                entity.HasIndex(c => c.QuestionId);
                entity.HasIndex(c => c.CreatedAt);
                entity.HasIndex(c => c.IsDeleted);

                entity.HasQueryFilter(c => !c.IsDeleted);
            });

        }
    }
}