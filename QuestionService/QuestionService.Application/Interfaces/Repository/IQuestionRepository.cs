
using QuestionService.Application.DTOs.QuestionDto;

namespace QuestionService.Application.Interfaces.Repository
{
    public interface IQuestionRepository
    {
        Task<bool> CreateQuestion(AddQuestionDto addQuestionDto, string userId);
        Task<ReadQuestionDto> GetFullQuestionById(string questionId);
        Task<List<ReadAbstractQuestionDto>> GetAllAbstractQuestion(string questionId);
        Task<List<ReadQuestionDto>> GetFullQuestion();
    }
}