using ConstrainService.API.ExceptionHandling;
using ConstrainService.Application.Interfaces.Service;
using ConstrainService.Application.Services;
using ConstrainService.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics;

namespace ConstrainService.API.Extensions
{
    public static class AppServiceExtension
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
            services.AddCorsConfiguration();
            services.AddApplicationServices();
            services.AddSingleton<IExceptionHandler,GlobalExceptionHandling>();
            services.AddExceptionHandler<GlobalExceptionHandling>();
        }

        private static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
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
            return services;
        }
        private static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {
            service.AddScoped<IConstrainServiceProvider, ConstrainServiceProvider>();
            return service;
        }
    }

}