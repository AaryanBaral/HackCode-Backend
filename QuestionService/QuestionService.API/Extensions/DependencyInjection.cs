
using QuestionService.API.ExceptionHandling;
using QuestionService.Application.Interfaces.Service;
using QuestionService.Application.Services;
using QuestionService.Infrastructure.DependencyInjection;


namespace QuestionService.API.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices();
            services.AddCorsConfiguration();
            services.AddExceptionHandler<GlobalExceptionHandling>();
            return services;
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
            service.AddScoped<IQuestionService, QuestionServices>();
            return service;
        }
    }
}