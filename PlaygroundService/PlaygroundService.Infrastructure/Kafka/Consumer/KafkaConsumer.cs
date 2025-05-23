using System.Collections.Concurrent;
using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlaygroundService.Application.Constants;
using PlaygroundService.Application.DTOs.KafkaDto;
using PlaygroundService.Application.Interfaces.Kafka;
using PlaygroundService.Infrastructure.Configurations.Kafka;

namespace PlaygroundService.Infrastructure.Kafka.Consumer
{

    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly IConsumer<Null, string> _consumer;

        private readonly ILogger<KafkaConsumer> _logger;
        private readonly string[] _topics;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<GetLanguageResponse>> _languageResponses = new();
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
                    case KafkaTopics.GetLanguageByName:
                        var message = JsonSerializer.Deserialize<GetLanguageResponse>(consumeResult.Message.Value)
                        ?? throw new ArgumentNullException("The givan value is null for validating user");
                        if (_languageResponses.TryRemove(correlationIDString, out var tcs))
                        {
                        }
                        break;
                }
            }
            _consumer.Close();
        }

        public Task<GetLanguageResponse> WaitForLanguageResponseAsync(string correlationID)
        {
            var tcs = new TaskCompletionSource<GetLanguageResponse>();
            _languageResponses.TryAdd(correlationID, tcs);
            return tcs.Task;
        }

    }
}