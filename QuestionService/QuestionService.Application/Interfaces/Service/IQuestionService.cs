using QuestionService.Application.DTOs.QuestionDto;

namespace QuestionService.Application.Interfaces.Service
{
    public interface IQuestionService
    {
        Task<bool> AddQuestionAsync(AddQuestionDto addQuestionDto, string userID);
        Task<bool> TestKafka();
        Task<bool> UpdateQuestion(UpdateQuestionDto updateQuestionDto, string id);
        Task<bool> DeleteQuestion(string id);
        Task<ReadQuestionDto> GetFullQuestionById(string questionId);
        Task<List<ReadAbstractQuestionDto>> GetAllAbstractQuestion();
        Task<List<ReadQuestionDto>> GetFullQuestions();
        Task<bool> DeleteQuestionPermanently(string id);
    }
}