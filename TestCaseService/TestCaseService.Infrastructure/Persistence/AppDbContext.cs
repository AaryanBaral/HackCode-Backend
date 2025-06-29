
using Microsoft.EntityFrameworkCore;
using TestCaseService.Domain.Entities;

namespace TestCaseService.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TestCase> TestCases { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TestCase>(entity =>
            {
                entity.HasKey(t => t.TestCaseId);
                entity.Property(t => t.TestCaseId)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(t => t.QuestionId)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(t => t.Input)
                    .IsRequired()
                    .HasColumnType("TEXT");

                entity.Property(t => t.ExpectedOutput)
                    .IsRequired()
                    .HasColumnType("TEXT");

                entity.Property(t => t.IsHidden)
                    .IsRequired();

                entity.Property(t => t.CreatedAt)
                    .IsRequired();

                entity.Property(t => t.UpdatedAt)
                    .IsRequired();

                entity.HasIndex(t => t.QuestionId);

            });
        }
    }
}