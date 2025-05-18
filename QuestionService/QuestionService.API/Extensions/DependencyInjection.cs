
using QuestionService.API.ExceptionHandling;
using QuestionService.Application.Interfaces;
using QuestionService.Application.Interfaces.Repository;
using QuestionService.Application.Services;
using QuestionService.Infrastructure.Configuration.Database;
using QuestionService.Infrastructure.Configuration.Jwt;
using QuestionService.Infrastructure.Configuration.Kafka;
using QuestionService.Infrastructure.DependencyInjection;
using QuestionService.Infrastructure.Kafka;
using QuestionService.Infrastructure.Kafka.Consumer;
using QuestionService.Infrastructure.Kafka.Producer;
using QuestionService.Infrastructure.Repository;

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