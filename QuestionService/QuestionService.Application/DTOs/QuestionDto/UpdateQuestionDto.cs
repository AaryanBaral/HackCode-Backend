using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionService.Domain.Enums;

namespace QuestionService.Application.DTOs.QuestionDto
{
    public class UpdateQuestionDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DifficultyEnum? Difficulty { get; set; }
        public string? TimeLimit { get; set; }
        public string? MemoryLimit { get; set; }
    }
}