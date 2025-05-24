using PlaygroundService.Application.Interfaces.Playground;
using PlaygroundService.Application.Services.Playground;
using PlaygroundService.Infrastructure.Configurations.Database;
using PlaygroundService.Infrastructure.Configurations.Jwt;
using PlaygroundService.Infrastructure.DependencyInjection;

namespace PlaygroundService.API.Extensions
{
    public static class AppServiceExtensions
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddCorsConfiguration();
            services.AddInfrastructure(configuration);
            services.AddSignalR();
            services.AddControllers();
            services.AddScoped<IPlaygroundService, PlaygroundServiceProvider>();
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