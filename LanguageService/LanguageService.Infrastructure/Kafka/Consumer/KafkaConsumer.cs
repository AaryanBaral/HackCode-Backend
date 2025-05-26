using System.Text.Json;
using Confluent.Kafka;
using LanguageService.Application.Constant;
using LanguageService.Application.DTOs.KafkaDto;
using LanguageService.Application.Interfaces.Kafka;
using LanguageService.Infrastructure.Configurations.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection; // Required for IServiceScope

namespace LanguageService.Infrastructure.Kafka.Consumer
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly IConsumer<Null, string> _consumer;
        private readonly ILogger<KafkaConsumer> _logger;
        private readonly string[] _topics;
        private readonly IServiceScopeFactory _scopeFactory;

        public KafkaConsumer(
            IOptions<KafkaOption> options,
            ILogger<KafkaConsumer> logger,
            string[] topics,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _topics = topics;
            _scopeFactory = scopeFactory;

            var config = options.Value;
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = config.BootstrapServers,
                GroupId = config.ConsumerGroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
            _consumer.Subscribe(topics);
        }

        public async Task ConsumeAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(5));
                if (consumeResult == null) continue;

                var correlationID = consumeResult.Message.Headers
                    .FirstOrDefault(h => h.Key == "correlationID")?.GetValueBytes() 
                    ?? throw new NullReferenceException("The header is null");
                var correlationIDString = System.Text.Encoding.UTF32.GetString(correlationID);

                using var scope = _scopeFactory.CreateScope();
                var kafkaService = scope.ServiceProvider.GetRequiredService<IKafkaService>();
                
                switch (consumeResult.Topic)
                {
                    case KafkaTopics.KafkaTest:
                        var test_message = JsonSerializer.Deserialize<string>(consumeResult.Message.Value);
                        Console.WriteLine($"Language Service is consuming message, and the message is: {test_message}");
                        break;

                    case KafkaTopics.GetLanguageByName:
                        Console.WriteLine("get-language-by-name being consumed");
                        var request = JsonSerializer.Deserialize<GetLanguageRequest>(consumeResult.Message.Value)
                            ?? throw new NullReferenceException("The message is null");
                        await kafkaService.GetLanguageByNameForKafka(request);
                        break;
                    
                    default:
                        Console.WriteLine("Test topic doesnot match");
                        break;
                }
            }
            _consumer.Close();
        }
    }
}
