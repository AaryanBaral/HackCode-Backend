using LanguageService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LanguageService.Infrastructure.Configurations.Database
{
    public static class DatabaseConfigurations
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment
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
                    .LogTo(Console.WriteLine, LogLevel.Information);
                if (environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                }
            });
            return services;
        }

    }
}