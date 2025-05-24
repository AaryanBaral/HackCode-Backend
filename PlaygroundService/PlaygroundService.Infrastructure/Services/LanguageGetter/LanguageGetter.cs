using System.Text.Json;
using PlaygroundService.Application.Constants;
using PlaygroundService.Application.DTOs.KafkaDto;
using PlaygroundService.Application.Interfaces.Kafka;
using PlaygroundService.Application.Interfaces.LanguageGetter;

namespace PlaygroundService.Infrastructure.Services.LanguageGetter
{
    public class LanguageGetter(IKafkaConsumer consumer, IKafkaProducer producer) : ILanguageGetter
    {
        private readonly IKafkaConsumer _consumer = consumer;
        private readonly IKafkaProducer _producer = producer;

        public async Task<ReadLanguageDto> GetLanguage(string language)
        {
            if (language is null) throw new NullReferenceException(nameof(language));
            var correlationId = Guid.NewGuid().ToString() ?? throw new NullReferenceException("the guid is generated null");
            var request = new GetLanguageRequest { Name = language, CorrelationID = correlationId };
            var awaitTask = _consumer.WaitForLanguageResponseAsync(correlationId);
            await _producer.ProduceAsync(KafkaTopics.GetLanguageByName, request, correlationId);
            var languageResponse = await awaitTask;
            if (languageResponse.Data is null) throw new NullReferenceException(nameof(languageResponse.Data));
            return languageResponse.Data;
        }

        public async Task<bool> KafkaTest()
        {
            await _producer.ProduceAsync("kafka-test", "this message is produced by QuestionService", "blabla");
            return true;
        }
    }
}