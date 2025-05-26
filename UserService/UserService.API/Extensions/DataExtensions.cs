using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Persistence;

namespace UserService.API.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlConnectionString = configuration.GetConnectionString("Default")
                ?? throw new InvalidOperationException("Connection string 'Default' not found.");

            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(sqlConnectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(60),
                        errorNumbersToAdd: null
                    );
                });

                options.EnableDetailedErrors()
                       .EnableSensitiveDataLogging()
                       .LogTo(Console.WriteLine, LogLevel.Information);
            });

            return services;
        }
    }
}
