using ConstrainService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConstrainService.Infrastructure.Configurations.Database
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            string mysqlConnString = configuration.GetConnectionString("MySqlConnection")?? throw new NullReferenceException("connection string is null");
            Console.WriteLine(mysqlConnString);


            services.AddDbContextFactory<AppDbContext>(options =>
            {
                options.UseMySQL(mysqlConnString, mysqlOptions =>
                    {
                        mysqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(60),
                            errorNumbersToAdd: null
                        );
                    })
                    .EnableDetailedErrors()
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging();
            });

            return services;
        }
    }
}