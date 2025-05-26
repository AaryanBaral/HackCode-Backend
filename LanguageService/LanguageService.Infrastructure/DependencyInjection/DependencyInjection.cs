using System.Collections;
using LanguageService.Application.Constant;
using LanguageService.Application.Interfaces;
using LanguageService.Application.Interfaces.Kafka;
using LanguageService.Application.Services;
using LanguageService.Infrastructure.Configurations.Kafka;
using LanguageService.Infrastructure.Kafka;
using LanguageService.Infrastructure.Kafka.Consumer;
using LanguageService.Infrastructure.Kafka.Producer;
using LanguageService.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LanguageService.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<ILanguageRepository, LanguageRepository>();
            service.AddKafka(configuration);
            service.AddScoped<ILanguageService, LanguageServiceProvider>();
        }

        private static void AddKafka(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<KafkaConsumerHostedService>();
            services.Configure<KafkaOption>(configuration.GetSection("kafka"));

            services.AddSingleton<IKafkaProducer, KafkaProducer>();
            services.AddSingleton<IKafkaConsumer, KafkaConsumer>();
            services.AddSingleton(KafkaTopics.GetKafkaTopics());

        }
    }
}