
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Core;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UserService.Application.Constants;
using UserService.Application.DTOs.KafkaDto;
using UserService.Application.Interfaces;
using UserService.Application.Interfaces.kafka;
using UserService.Infrastructure.configurations.Kafka;

namespace UserService.Infrastructure.Kafka
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly IConsumer<Null, string> _consumer;
        private readonly string[] _topics;
        private readonly ILogger<KafkaConsumer> _logger;
        private readonly IAuthRepository _authRepository;
        private readonly IKafkaProducer _producer;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<ValidateUserIdResponse>> _userIDResponses = new();
        public KafkaConsumer(IOptions<KafkaOption> options, string[] topics, ILogger<KafkaConsumer> logger
        , IAuthRepository authRepository, IKafkaProducer producer)
        {
            _logger = logger;
            var config = options.Value;
            var bootstrapServers = config.BootstrapServers;
            var groupId = config.ConsumerGroupId;
            _topics = topics;
            _authRepository = authRepository;
            _producer = producer;

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
            _consumer.Subscribe(topics);

        }
        public async Task ConsumeAsync(CancellationToken cancellationToken)
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
                        Console.WriteLine($"User Service is consuming message, and the message is :{test_message}");
                        break;
                    case KafkaTopics.ValidateUserId:
                        var validateUserIdData = JsonSerializer.Deserialize<ValidateUserIdRequest>(consumeResult.Message.Value);
                        await IsUserIdValid(validateUserIdData);
                        break;
                    case KafkaTopics.ValidateUserRole:
                        var validateUserRoleData = JsonSerializer.Deserialize<ValidateUserRoleRequest>(consumeResult.Message.Value);
                        await UserRoleValid(validateUserRoleData);
                        break;
                }
            }
        }


        public async Task IsUserIdValid(ValidateUserIdRequest request)
        {
            var result = await _authRepository.GetUserById(request.UserID);
            var existingUser = result != null;

            var response = new ValidateUserIdResponse()
            {
                IsValid = existingUser,
                CorrelationID = request.CorrelationID,
                Message = "Result"
            };
            await _producer.ProduceAsync(KafkaTopics.ValidateUserIdResponse, response, request.CorrelationID);
        }

        public async Task UserRoleValid(ValidateUserRoleRequest roleRequest)
        {
            var result = await _authRepository.GetUserRoleAsync(roleRequest.UserId);
            var currentRole = result != null;

            var response = new ValidateUserRoleResponse()
            {
                IsValid = currentRole,
                CorrelationID = roleRequest.CorrelationID,
                Message = "currentRole"
            };
            await _producer.ProduceAsync(KafkaTopics.ValidateUserRoleResponse, response, roleRequest.CorrelationID);
        }

     
    }
    
}