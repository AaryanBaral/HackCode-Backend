
using Microsoft.Extensions.Hosting;
using PlaygroundService.Application.Interfaces.Kafka;

namespace PlaygroundService.Infrastructure.Kafka
{
    public class KafkaConsumerHostedService(IKafkaConsumer kafkaConsumer) : BackgroundService
    {
        private readonly IKafkaConsumer _kafkaConsumer = kafkaConsumer;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await Task.Run(()=>{
                _kafkaConsumer.ConsumeAsync(stoppingToken);
            }, stoppingToken);
        }
    }
}