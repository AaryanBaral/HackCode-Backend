
using ConstrainService.Application.Constants;
using ConstrainService.Application.DTOs.Kafka;
using ConstrainService.Application.Interfaces.Kafka;

namespace ConstrainService.Application.Services.Kafka
{
    public class QuestionValidator(IKafkaConsumer consumer, IKafkaProducer producer):IQuestionValidator
    {
        private readonly IKafkaConsumer _consumer = consumer;
        private readonly IKafkaProducer _producer = producer;

        public async Task<bool> ValidateQuestionId(string id)
        {
            if (id is null) throw new NullReferenceException(nameof(id));
            var correlationId = Guid.NewGuid().ToString() ?? throw new NullReferenceException("the guid is generated null");
            var request = new ValidateQuestionRequest { QuestionId = id, CorrelationID = correlationId };
            var awaitTask = _consumer.WaitForValidateQuestionIdResponseAsync(correlationId);
            await _producer.ProduceAsync(KafkaTopics.ValidateQuestionId, request, correlationId);
            var languageResponse = await awaitTask;
            return languageResponse.IsValid;
        }
    }
}