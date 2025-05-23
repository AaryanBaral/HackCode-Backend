using LanguageService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageService.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Language>(entity =>
            {
                entity.HasKey(q => q.Id);

                entity.Property(q => q.Id)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(q => q.Name)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(q => q.DockerImage)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(q => q.FileExtension)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(q => q.CompileCommand)
                    .HasMaxLength(255);

                entity.Property(q => q.ExecuteCommand)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(q => q.CreatedAt)
                    .HasDefaultValueSql("NOW()");

                entity.Property(q => q.ModifiedAt)
                      .HasDefaultValueSql("NOW()");

            });
        }
    }
}