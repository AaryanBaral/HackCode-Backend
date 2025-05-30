
using QuestionService.Application.DTOs.QuestionDto;
using QuestionService.Domain.Entities;

namespace QuestionService.Infrastructure.Mappers
{
    public static class QuestionMapper
    {

        public static Question ToQueston(this AddQuestionDto addQuestionDto, string createdBy)
        {
            return new Question()
            {
                Title = addQuestionDto.Title,
                Description = addQuestionDto.Description,
                Difficulty = addQuestionDto.Difficulty,
                MemoryLimit = addQuestionDto.MemoryLimit,
                TimeLimit = addQuestionDto.TimeLimit,
                CreatedBy = createdBy,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static ReadQuestionDto ToReadQuestionDto(this Question question)
        {
            return new ReadQuestionDto()
            {
                QuestionId = question.QuestionId,
                Title = question.Title,
                Description = question.Description,
                Difficulty = question.Difficulty,
                MemoryLimit = question.MemoryLimit,
                TimeLimit = question.TimeLimit,
                CreatedBy = question.CreatedBy,
                UpdatedAt = question.UpdatedAt
            };
        }
        public static ReadAbstractQuestionDto ToReadAbstractQuestionDto(this Question question)
        {
            return new ReadAbstractQuestionDto()
            {
                QuestionId = question.QuestionId,
                Title = question.Title,
                Difficulty = question.Difficulty,
            };
        }
        public static void UpdateQuestion(this Question question, UpdateQuestionDto updateQuestionDto)
        {
            question.Title = updateQuestionDto.Title ?? question.Title;
            question.Description = updateQuestionDto.Title ?? question.Description;
            question.TimeLimit = updateQuestionDto.TimeLimit ?? question.TimeLimit;
            question.MemoryLimit = updateQuestionDto.MemoryLimit ?? question.MemoryLimit;
            question.Difficulty = updateQuestionDto.Difficulty ?? question.Difficulty;

        }
    }
}