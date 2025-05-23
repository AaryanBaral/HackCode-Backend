
using System.Text.Json;

using Confluent.Kafka;

using LanguageService.Application.Interfaces.Kafka;
using LanguageService.Infrastructure.Configurations.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LanguageService.Infrastructure.Kafka.Consumer
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly IConsumer<Null, string> _consumer;

        private readonly ILogger<KafkaConsumer> _logger;
        private readonly string[] _topics;
        public KafkaConsumer(IOptions<KafkaOption> options, ILogger<KafkaConsumer> logger, string[] topics)
        {
            _logger = logger;
            var config = options.Value;
            var bootstrapServers = config.BootstrapServers;
            var groupId = config.ConsumerGroupId;
            _topics = topics;
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
            _consumer.Subscribe(topics);

        }

        public void ConsumeAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(5));
                if (consumeResult == null) continue;
                var correlationID = consumeResult.Message.Headers
                .FirstOrDefault(h => h.Key == "correlationID")?.GetValueBytes() ?? throw new NullReferenceException("The header is null");
                var correlationIDString = System.Text.Encoding.UTF8.GetString(correlationID);
                switch (consumeResult.Topic)
                {

                    case "kafka-test":
                        var test_message = JsonSerializer.Deserialize<string>(consumeResult.Message.Value);
                        Console.WriteLine($"Question Service is consuming message, and the message is :{test_message}");
                        break;
                    case "get-language-by-name":

                        break;
                }
            }
            _consumer.Close();
        }

        
    }
}