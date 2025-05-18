using PlaygroundService.Infrastructure.Configurations.Database;
using PlaygroundService.Infrastructure.Configurations.Jwt;

namespace PlaygroundService.API.Extensions
{
    public static class AppServiceExtensions
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtAuthentication(configuration);
            services.AddDatabase(configuration);
            services.AddCorsConfiguration();

        }

        private static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {

                options.AddPolicy("AllowAny", builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "http://localhost:3001")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });
        }
    }
}