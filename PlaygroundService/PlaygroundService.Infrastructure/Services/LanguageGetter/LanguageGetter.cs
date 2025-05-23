using PlaygroundService.Application.Constants;
using PlaygroundService.Application.DTOs.KafkaDto;
using PlaygroundService.Application.Interfaces.Kafka;
using PlaygroundService.Application.Interfaces.LanguageGetter;

namespace PlaygroundService.Infrastructure.Services.LanguageGetter
{
    public class LanguageGetter(IKafkaConsumer consumer, IKafkaProducer producer):ILanguageGetter
    {
        private readonly IKafkaConsumer _consumer = consumer;
        private readonly IKafkaProducer _producer = producer;


        public async Task<GetLanguageResponse> GetLanguage(string language)
        {
            var correlationID = Guid.NewGuid().ToString() ?? throw new NullReferenceException("the guid is generated null");
            var request = new GetLanguageRequest { Name = language, CorrelationID = correlationID };
            await _producer.ProduceAsync(KafkaTopics.GetLanguageByName, request, correlationID);
            var languageResponse = await _consumer.WaitForLanguageResponseAsync(correlationID);
            return languageResponse;
        }
    }
}