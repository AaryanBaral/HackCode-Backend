using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using UserService.Application.Interfaces.kafka;

namespace UserService.Infrastructure.Kafka
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