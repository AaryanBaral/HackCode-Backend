using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuestionService.Infrastructure.Configuration.Kafka;
using QuestionService.Infrastructure.Kafka;
using QuestionService.Infrastructure.Kafka.Consumer;
using QuestionService.Infrastructure.Kafka.Producer;
using QuestionService.Infrastructure.Configuration.Jwt;
using QuestionService.Infrastructure.Configuration.Database;
using QuestionService.Application.Interfaces.Repository;
using QuestionService.Infrastructure.Repository;
using QuestionService.Application.Interfaces.Kafka;
namespace QuestionService.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
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
            service.AddScoped<IQuestionRepository, QuestionRepository>();
            return service;
        }
    }
}