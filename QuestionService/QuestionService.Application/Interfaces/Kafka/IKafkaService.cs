using QuestionService.Application.DTOs.KafkaDto;

namespace QuestionService.Application.Interfaces.Kafka
{
    public interface IKafkaService
    {
        Task<ValidateUserIDResponse> ValidateUserIdRequest(string userID);
    }
}