using QuestionService.Application.DTOs.QuestionDto;

namespace QuestionService.Application.Interfaces.Service
{
    public interface IQuestionService
    {
        Task AddQuestionAsync(AddQuestionDto addQuestionDto, string userID);
        Task<bool> TestKafka();
        Task UpdateQuestion(UpdateQuestionDto updateQuestionDto, string id);
        Task DeleteQuestion(string id);
        Task<ReadQuestionDto> GetFullQuestionById(string questionId);
        Task<List<ReadAbstractQuestionDto>> GetAllAbstractQuestion();
        Task<List<ReadQuestionDto>> GetFullQuestions();
        Task DeleteQuestionPermanently(string id);
        Task<bool> ValidateQuestionByID(string id);
    }
}