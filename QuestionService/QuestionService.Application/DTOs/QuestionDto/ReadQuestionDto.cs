using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionService.Domain.Enums;

namespace QuestionService.Application.DTOs.QuestionDto
{

    public class ReadListQuestionDto
    {
        public required string QuestionId { get; set; }
        public required string Title { get; set; }
        public required string Difficulty { get; set; }

    }
    public class ReadQuestionDto
    {

        public required string QuestionId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DifficultyEnum Difficulty { get; set; }
        public required string TimeLimit { get; set; }
        public required string MemoryLimit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public required string CreatedBy { get; set; }
    }
    public class ReadSingleQuestionDto
    {
        public required string QuestionId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DifficultyEnum Difficulty { get; set; }
        public required string TimeLimit { get; set; }
        public required string MemoryLimit { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}