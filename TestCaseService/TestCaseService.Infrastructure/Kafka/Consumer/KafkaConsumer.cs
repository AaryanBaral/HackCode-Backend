using System.Collections.Concurrent;
using System.Text.Json;
using Confluent.Kafka;
using ConstrainService.Application.DTOs.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestCaseService.Application.Constants;
using TestCaseService.Application.Interfaces.Kafka;
using TestCaseService.Infrastructure.Configurations.Kafka;

namespace TestCaseService.Infrastructure.Kafka.Consumer
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly IConsumer<Null, string> _consumer;
        private readonly string[] _topics;
        private readonly ILogger<KafkaConsumer> _logger;
                private readonly ConcurrentDictionary<string, TaskCompletionSource<ValidateQuestionResponse>> _validateQuestionResponse = new();
        public KafkaConsumer(IOptions<KafkaOptions> options, string[] topics, ILogger<KafkaConsumer> logger)
        {
            _logger = logger;
            var config = options.Value;
            var bootstrapServers = config.BootstrapServers;
            var groupId = config.ConsumerGroupId;
            _topics = topics;

            if (string.IsNullOrEmpty(bootstrapServers))
                throw new ArgumentNullException(nameof(bootstrapServers), "Kafka:BootstrapServers is not configured.");
            if (string.IsNullOrEmpty(groupId))
                throw new ArgumentNullException(nameof(groupId), "Kafka:GroupId is not configured.");

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
            _consumer.Subscribe(_topics);
        }
        public void ConsumeAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(5));
                if (consumeResult == null) continue;
                var correlationID = consumeResult.Message.Headers
                .FirstOrDefault(h => h.Key == "correlationID")?.GetValueBytes() ?? throw new NullReferenceException("The header is null");
                var correlationIDString = System.Text.Encoding.UTF32.GetString(correlationID);
                switch (consumeResult.Topic)
                {
                    case KafkaTopics.KafkaTest:
                        var test_message = JsonSerializer.Deserialize<string>(consumeResult.Message.Value);
                        Console.WriteLine($"Testcase Service is consuming message, and the message is :{test_message}");
                        break;
                }
            }
        }
        public Task<ValidateQuestionResponse> WaitForValidateQuestionIdResponseAsync(string correlationID)
        {
            var tcs = new TaskCompletionSource<ValidateQuestionResponse>();
            _validateQuestionResponse.TryAdd(correlationID, tcs);
            return tcs.Task;
        }

    }

}