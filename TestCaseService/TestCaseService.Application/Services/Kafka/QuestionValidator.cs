using ConstrainService.Application.DTOs.Kafka;
using TestCaseService.Application.Constants;
using TestCaseService.Application.Interfaces.Kafka;

namespace TestCaseService.Application.Services.Kafka
{
    public class QuestionValidator(IKafkaConsumer consumer, IKafkaProducer producer) : IQuestionValidator
    {
        private readonly IKafkaConsumer _consumer = consumer;
        private readonly IKafkaProducer _producer = producer;
        public async Task<bool> ValidateQuestionId(string id)
        {
            var correlationId = Guid.NewGuid().ToString();
            var request = new ValidateQuestionRequest { QuestionId = id, CorrelationID = correlationId };
            var awaitTask = _consumer.WaitForValidateQuestionIdResponseAsync(correlationId);
            await _producer.ProduceAsync(KafkaTopics.ValidateQuestionId, request, correlationId);
            var resposne = await awaitTask;
            return resposne.IsValid;
        }
    }
}