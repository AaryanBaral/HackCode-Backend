using Microsoft.EntityFrameworkCore;
using QuestionService.Application.DTOs.QuestionDto;
using QuestionService.Application.Interfaces.Repository;
using QuestionService.Domain.Entities;
using QuestionService.Infrastructure.Mappers;
using QuestionService.Infrastructure.Persistence;

namespace QuestionService.Infrastructure.Repository
{

    public class QuestionRepository(AppDbContext context) : IQuestionRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> CreateQuestion(AddQuestionDto addQuestionDto, string userId)
        {

            // user id is already validated 
            Question question = QuestionMapper.ToQueston(addQuestionDto, userId);
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReadQuestionDto> GetFullQuestionById(string questionId)
        {
            var question = await _context.Questions.FindAsync(questionId) ?? throw new KeyNotFoundException("Given Question Id is not valid");
            return question.ToReadQuestionDto();
        }
        public async Task<List<ReadAbstractQuestionDto>> GetAllAbstractQuestion(string questionId)
        {
            var question = await _context.Questions.ToListAsync() ?? throw new NullReferenceException("No Questions available");
            return [.. question.Select(q => q.ToReadAbstractQuestionDto())];
        }

        public async Task<List<ReadQuestionDto>> GetFullQuestion()
        {
            var question = await _context.Questions.ToListAsync() ?? throw new KeyNotFoundException("Given Question Id is not valid");
            return [.. question.Select(q => q.ToReadQuestionDto())];
        }


    }
}
