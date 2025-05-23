using LanguageService.API.ExceptionHandling;
using LanguageService.Infrastructure.Configurations.Database;
using LanguageService.Infrastructure.Configurations.Jwt;
using LanguageService.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics;

namespace LanguageService.API.Extensions
{
    public static class AppServiceExtensions
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.AddAuthorization();
            services.AddJwtAuthentication(configuration);
            services.AddInfrastructure(configuration);
            services.AddDatabase(configuration, environment);
            services.AddCorsConfiguration();
            services.AddSingleton<IExceptionHandler,GlobalExceptionHandling>();

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