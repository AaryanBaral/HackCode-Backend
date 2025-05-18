using QuestionService.Domain.Enums;

namespace QuestionService.Application.DTOs.QuestionDto
{
    public class AddQuestionDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DifficultyEnum Difficulty { get; set; }
        public required string TimeLimit { get; set; }
        public required string MemoryLimit { get; set; }
    }
}