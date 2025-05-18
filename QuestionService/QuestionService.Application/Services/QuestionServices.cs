
using QuestionService.Application.DTOs.KafkaDto;
using QuestionService.Application.DTOs.QuestionDto;
using QuestionService.Application.Interfaces;
using QuestionService.Application.Interfaces.Kafka;
using QuestionService.Application.Interfaces.Repository;

namespace QuestionService.Application.Services
{
 public class QuestionServices(IKafkaProducer producer, IKafkaConsumer responseConsumer, IQuestionRepository repository):IQuestionService
    {
        private  readonly IKafkaProducer _producer = producer;
        private  readonly IKafkaConsumer _responseConsumer = responseConsumer;
        private  readonly IQuestionRepository _repository = repository;
        public async Task<bool> AddQuestionAsync(AddQuestionDto addQuestionDto, string userID)

        {
            // Generate unique correlation ID
            var correlationID = Guid.NewGuid().ToString() ?? throw new NullReferenceException("the guid is generated null");

            // Send validation request
            var request = new ValidateUserIdRequest { UserID = userID, CorrelationID = correlationID };
            await _producer.ProduceAsync("validateUserID-request", request, correlationID);

            // Wait for response
            var response = await _responseConsumer.WaitForUserIDResponseAsync(correlationID);

            if (!response.IsValid)
            {
                throw new KeyNotFoundException($"Given UserId is not valid {response.Message}");
            }

            
            await _repository.CreateQuestion(addQuestionDto, userID);

            return true;
        }

        public async Task<bool> TestKafka (){
            await _producer.ProduceAsync("kafka-test","this message is produced by QuestionService","blabla");
            return true;
        }
    }
}