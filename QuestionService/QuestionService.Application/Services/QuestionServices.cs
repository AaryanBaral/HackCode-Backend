
using PlaygroundService.Application.Constants;
using QuestionService.Application.DTOs.KafkaDto;
using QuestionService.Application.DTOs.QuestionDto;
using QuestionService.Application.Interfaces.Service;
using QuestionService.Application.Interfaces.Kafka;
using QuestionService.Application.Interfaces.Repository;

namespace QuestionService.Application.Services
{
    public class QuestionServices(IKafkaProducer producer, IKafkaConsumer responseConsumer, IQuestionRepository repository) : IQuestionService
    {
        private readonly IKafkaProducer _producer = producer;
        private readonly IKafkaConsumer _responseConsumer = responseConsumer;
        private readonly IQuestionRepository _repository = repository;
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

        public async Task<bool> TestKafka()
        {
            await _producer.ProduceAsync("kafka-test", "this message is produced by QuestionService", "blabla");
            return true;
        }

        public async Task<bool> UpdateQuestion(UpdateQuestionDto updateQuestionDto, string id)
        {
            await _repository.UpdateQuestion(updateQuestionDto, id);
            return true;
        }
        public async Task<bool> DeleteQuestion(string id)
        {
            await _repository.DeleteQuestion(id);
            return true;
        }
        public async Task<ReadQuestionDto> GetFullQuestionById(string questionId)
        {
            var question = await _repository.GetFullQuestionById(questionId);
            return question;
        }
        public async Task<List<ReadAbstractQuestionDto>> GetAllAbstractQuestion()
        {
            var questions = await _repository.GetAllAbstractQuestion();
            return questions;
        }
        public async Task<List<ReadQuestionDto>> GetFullQuestions()
        {
            var questions = await _repository.GetFullQuestions();
            return questions;
        }
        public async Task<bool> DeleteQuestionPermanently(string id)
        {
            await _repository.DeleteQuestionPermanently(id);
            await DeleteQuestionKafka(id);
            return true;
        }

        private async Task<QuestionDeleteResponse> DeleteQuestionKafka(string id)
        {
            var correlationId = Guid.NewGuid().ToString() ?? throw new NullReferenceException("the guid is generated null");
            var request = new QuestionDeleteRequest()
            {
                CorrelationID = correlationId,
                QuestionId = id
            };
            var deleteQuestionResponse = _responseConsumer.WaitForQuestionDeleteResponseAsync(correlationId);
            await _producer.ProduceAsync(KafkaTopics.DeleteQuestionRequest, request, correlationId);
            var result = await deleteQuestionResponse;
            return result;
        }
        
    }
}