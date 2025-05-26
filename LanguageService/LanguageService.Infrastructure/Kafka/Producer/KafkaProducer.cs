
using System.Text.Json;

using Confluent.Kafka;
using LanguageService.Application.Interfaces.Kafka;
using LanguageService.Infrastructure.Configurations.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LanguageService.Infrastructure.Kafka.Producer
{
    public class KafkaProducer:IKafkaProducer
    {
        private readonly IProducer<Null, string> _producer ;

        private readonly ILogger<KafkaProducer> _logger ;

        public KafkaProducer(IOptions<KafkaOption> options, ILogger<KafkaProducer> logger)
        {
            var config = options.Value;
            _logger = logger;
            var producerConfig = new ProducerConfig()
            {
                BootstrapServers = config.BootstrapServers,
                ClientId = config.ProducerClientId
            };
            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        public async Task ProduceAsync<T>(string topic, T message, string correlationID)
        {
            Console.WriteLine("Inside Producer");
            Console.WriteLine(JsonSerializer.Serialize(message));
            var kafkaMessage = new Message<Null, string>()
            {
                Value = JsonSerializer.Serialize(message),
                Headers = [new Header("correlationID", System.Text.Encoding.UTF32.GetBytes(correlationID))]
            };
            await _producer.ProduceAsync(topic, kafkaMessage);
            _logger.LogInformation("Produced message to topic {Topic}: {Message}", topic, kafkaMessage);

        }
        public void Dispose()
        {
            _producer.Flush(TimeSpan.FromSeconds(10));
            _producer.Dispose();
        }
    }
}