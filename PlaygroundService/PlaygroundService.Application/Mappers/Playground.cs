using PlaygroundService.Application.DTOs.PlaygroundDtos;
using PlaygroundService.Domain.Entities;

namespace PlaygroundService.Application.Mappers
{
    public static class QuestionMapper
    {
        public static Playground ToPlayground(this AddPlaygroundDto addPlaygroundDto)
        {
            return new Playground()
            {
                Language = addPlaygroundDto.Language,
                Code = addPlaygroundDto.Code,
                UserId = addPlaygroundDto.UserId,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}