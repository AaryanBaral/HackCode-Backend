using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlaygroundService.Application.Interfaces.Command;
using PlaygroundService.Application.Interfaces.Container;
using PlaygroundService.Application.Interfaces.Kafka;
using PlaygroundService.Application.Interfaces.LanguageGetter;
using PlaygroundService.Infrastructure.Command;
using PlaygroundService.Infrastructure.Configurations.Docker;
using PlaygroundService.Infrastructure.Configurations.Kafka;
using PlaygroundService.Infrastructure.Kafka;
using PlaygroundService.Infrastructure.Kafka.Consumer;
using PlaygroundService.Infrastructure.Kafka.Producer;
using PlaygroundService.Infrastructure.Services.Container;
using PlaygroundService.Infrastructure.Services.LanguageGetter;
using PlaygroundService.Infrastructure.Configurations.Database;
using PlaygroundService.Infrastructure.Configurations.Jwt;
using PlaygroundService.Application.Constants;

namespace PlaygroundService.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddServices();
            service.AddKafka(configuration);
            service.AddDatabase(configuration);
            service.AddJwtAuthentication(configuration);
            
        }

        public static void AddServices(this IServiceCollection service)
        {
            service.AddScoped<IDockerClientFactory, DockerClientFactory>();
            service.AddScoped<ICommandExecutor, CommandExecutor>();
            service.AddScoped<IContainerManager, ContainerManager>();
            service.AddScoped<ILanguageGetter, LanguageGetter>();
        }
        public static void AddKafka(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHostedService<KafkaConsumerHostedService>();

            //  adding the kafka config to the di and mapping it to the section of the appsetting.json
            //  so that it can be accessed using Ioption anywahere over the app
            service.Configure<KafkaOption>(configuration.GetSection("kafka"));
            service.AddSingleton(KafkaTopics.GetKafkaTopics());
            service.AddSingleton<IKafkaProducer, KafkaProducer>();
            service.AddSingleton<IKafkaConsumer, KafkaConsumer>();
        }


    }
}