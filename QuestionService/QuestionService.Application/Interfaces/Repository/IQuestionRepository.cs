
using QuestionService.Application.DTOs.QuestionDto;
using QuestionService.Domain.Entities;

namespace QuestionService.Application.Interfaces.Repository
{
    public interface IQuestionRepository
    {
        Task<bool> CreateQuestion(Question question);
        Task<Question?> GetFullQuestionById(string questionId);
        Task<List<Question>> GetAllQuestions();
        Task UpdateQuestion(Question question);
        Task DeleteQuestion(string id);
        Task DeleteQuestionPermanently(string id);
        Task<bool> ValidateQuestion(string id);
    }
}