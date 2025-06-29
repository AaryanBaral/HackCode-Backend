using TestCaseService.Domain.Entities;
using TestCaseService.Application.DTOs.TetstCase;

namespace TestCaseService.Application.Mapper
{
    public static class TestCaseMapper
    {
        public static TestCase ToEntity(this CreateTestCaseDto dto)
        {
            return new TestCase
            {
                QuestionId = dto.QuestionId,
                Input = dto.Input,
                ExpectedOutput = dto.ExpectedOutput,
                IsHidden = dto.IsHidden,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static ReadTestCaseDto ToReadDto(this TestCase entity)
        {
            return new ReadTestCaseDto
            {
                TestCaseId = entity.TestCaseId,
                QuestionId = entity.QuestionId,
                Input = entity.Input,
                ExpectedOutput = entity.ExpectedOutput,
                IsHidden = entity.IsHidden,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static void ApplyUpdate(this TestCase entity, UpdateTestCaseDto dto)
        {
            entity.Input = dto.Input;
            entity.ExpectedOutput = dto.ExpectedOutput;
            entity.IsHidden = dto.IsHidden;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
