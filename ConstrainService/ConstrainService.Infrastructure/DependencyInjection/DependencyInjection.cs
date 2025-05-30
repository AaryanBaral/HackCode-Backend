using ConstrainService.Application.Interfaces.Kafka;
using ConstrainService.Application.Interfaces.Repository;
using ConstrainService.Infrastructure.Configurations.Kafka;
using ConstrainService.Infrastructure.Kafka;
using ConstrainService.Infrastructure.Kafka.Consumer;
using ConstrainService.Infrastructure.Kafka.Producer;
using ConstrainService.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ConstrainService.Infrastructure.Configurations.Database;
using ConstrainService.Infrastructure.Configurations.Jwt;

namespace ConstrainService.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtAuthentication(configuration);
            services.AddKafkaService(configuration);
            services.AddDatabase(configuration);
            services.AddRepository();
        }
        private static void AddKafkaService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<KafkaConsumerHostedService>();

            //  adding the kafka config to the di and mapping it to the section of the appsetting.json
            //  so that it can be accessed using Ioption anywahere over the app
            services.Configure<KafkaOption>(configuration.GetSection("kafka"));

            services.AddSingleton<IKafkaProducer, KafkaProducer>();
            services.AddSingleton<IKafkaConsumer, KafkaConsumer>();
            string[] kafkaTopics = ["validateUserID-request", "other-topic"];
            services.AddSingleton(kafkaTopics);
        }
        private static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddScoped<IConstrainRepository, ConstrainRepository>();
            return service;
        }
    }
}