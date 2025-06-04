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
        public async Task<List<ReadAbstractQuestionDto>> GetAllAbstractQuestion()
        {
            var question = await _context.Questions.ToListAsync();
            if (question.Count == 0)
                throw new NullReferenceException(nameof(question));

            return [.. question.Select(q => q.ToReadAbstractQuestionDto())];
        }

        public async Task<List<ReadQuestionDto>> GetFullQuestions()
        {
            var question = await _context.Questions.ToListAsync();
            if (question.Count == 0)
                throw new NullReferenceException(nameof(question));
            return [.. question.Select(q => q.ToReadQuestionDto())];
        }

        public async Task<bool> UpdateQuestion(UpdateQuestionDto updateQuestionDto, string id)
        {
            var question = await _context.Questions.FindAsync(id) ?? throw new KeyNotFoundException("Given Question Id is not valid");
            question.UpdateQuestion(updateQuestionDto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteQuestion(string id)
        {
            var question = await _context.Questions.FindAsync(id) ?? throw new KeyNotFoundException("Given Question Id is not valid");
            question.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteQuestionPermanently(string id)
        {
            var question = await _context.Questions.FindAsync(id) ?? throw new KeyNotFoundException("Given Question Id is not valid");
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
