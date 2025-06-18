using QuestionService.Application.DTOs.KafkaDto;
using QuestionService.Application.Interfaces.Kafka;
using QuestionService.Application.Interfaces.Repository;

namespace QuestionService.Application.Services.Kafka
{
    public class KafkaService(IQuestionRepository questionRepository, IKafkaProducer producer, IKafkaConsumer consumer):IKafkaService
    {
        private readonly IKafkaProducer _producer = producer;
        private readonly IQuestionRepository _repository = questionRepository;
        private readonly IKafkaConsumer _consumer = consumer;

        public async Task<ValidateUserIDResponse> ValidateUserIdRequest(string userID)
        {

            // Generate unique correlation ID
            var correlationID = Guid.NewGuid().ToString() ?? throw new NullReferenceException("the guid is generated null");

            // Send validation request
            var request = new ValidateUserIdRequest { UserID = userID, CorrelationID = correlationID };
            await _producer.ProduceAsync("validateUserID-request", request, correlationID);

            // Wait for response
            var response = await _consumer.WaitForUserIDResponseAsync(correlationID);
            return response;
        }
    }
}