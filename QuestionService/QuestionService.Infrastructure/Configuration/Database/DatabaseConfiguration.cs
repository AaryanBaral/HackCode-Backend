using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuestionService.Infrastructure.Persistence;

namespace QuestionService.Infrastructure.Configuration.Database
{
    public static class DatabaseConfiguration
    {
        public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();
        }

        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var postgresConnString = configuration.GetConnectionString("PostgresConnection");
            Console.WriteLine(postgresConnString);
            services.AddDbContextFactory<AppDbContext>(options =>
            {
                options.UseNpgsql(postgresConnString, npgsqlOptions =>
                        npgsqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(60),
                            errorCodesToAdd: null
                        )
                )
                .EnableDetailedErrors()
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging(); // Enable sensitive data logging here
            });



            return services;
        }
    }
}