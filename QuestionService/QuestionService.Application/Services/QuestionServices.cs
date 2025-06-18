using PlaygroundService.Application.Constants;
using QuestionService.Application.DTOs.KafkaDto;
using QuestionService.Application.DTOs.QuestionDto;
using QuestionService.Application.Interfaces.Service;
using QuestionService.Application.Interfaces.Kafka;
using QuestionService.Application.Interfaces.Repository;
using QuestionService.Application.Mappers;


namespace QuestionService.Application.Services
{
    public class QuestionServices(IKafkaProducer producer,
    IKafkaConsumer responseConsumer,
    IQuestionRepository repository,
    IKafkaService kafkaService) : IQuestionService
    {
        private readonly IKafkaProducer _producer = producer;
        private readonly IKafkaConsumer _responseConsumer = responseConsumer;
        private readonly IQuestionRepository _repository = repository;
        private readonly IKafkaService _kafkaService = kafkaService;
        public async Task AddQuestionAsync(AddQuestionDto addQuestionDto, string userID)
        {
            
            var response = await _kafkaService.ValidateUserIdRequest(userID);

            if (!response.IsValid)
            {
                throw new KeyNotFoundException($"Given UserId is not valid {response.Message}");
            }
            var question = addQuestionDto.ToQueston(userID);

            await _repository.CreateQuestion(question);

        }

        public async Task<bool> TestKafka()
        {
            await _producer.ProduceAsync("kafka-test", "this message is produced by QuestionService", "blabla");
            return true;
        }

        public async Task UpdateQuestion(UpdateQuestionDto updateQuestionDto, string id)
        {
            var question = await _repository.GetFullQuestionById(id) ?? throw new KeyNotFoundException("question of given id not found");
            question.UpdateQuestion(updateQuestionDto);
            await _repository.UpdateQuestion(question);
        }
        public async Task DeleteQuestion(string id)
        {
            await _repository.DeleteQuestion(id);
        }
        public async Task<ReadQuestionDto> GetFullQuestionById(string questionId)
        {
            var question = await _repository.GetFullQuestionById(questionId) ?? throw new KeyNotFoundException("question of given id not found");
            return question.ToReadQuestionDto();
        }
        public async Task<List<ReadAbstractQuestionDto>> GetAllAbstractQuestion()
        {
            var questions = await _repository.GetAllQuestions();
            return [.. questions.Select(q => q.ToReadAbstractQuestionDto())];
        }
        public async Task<List<ReadQuestionDto>> GetFullQuestions()
        {
            var questions = await _repository.GetAllQuestions();
            return [.. questions.Select(q => q.ToReadQuestionDto())];
        }
        public async Task DeleteQuestionPermanently(string id)
        {
            await _repository.DeleteQuestionPermanently(id);
            await DeleteQuestionKafka(id);
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

        public async Task<bool> ValidateQuestionByID(string id)
        {
            return await _repository.ValidateQuestion(id);
        }


    }
}