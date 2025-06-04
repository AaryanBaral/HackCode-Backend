
using QuestionService.Application.DTOs.QuestionDto;

namespace QuestionService.Application.Interfaces.Repository
{
    public interface IQuestionRepository
    {
        Task<bool> CreateQuestion(AddQuestionDto addQuestionDto, string userId);
        Task<ReadQuestionDto> GetFullQuestionById(string questionId);
        Task<List<ReadAbstractQuestionDto>> GetAllAbstractQuestion();
        Task<List<ReadQuestionDto>> GetFullQuestions();
        Task<bool> UpdateQuestion(UpdateQuestionDto updateQuestionDto, string id);
        Task<bool> DeleteQuestion(string id);
        Task<bool> DeleteQuestionPermanently(string id);
    }
}