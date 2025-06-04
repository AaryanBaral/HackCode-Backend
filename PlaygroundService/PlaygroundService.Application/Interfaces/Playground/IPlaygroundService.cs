using PlaygroundService.Application.DTOs.KafkaDto;

namespace PlaygroundService.Application.Interfaces.Playground
{
    public interface IPlaygroundService
    {
        Task<ReadLanguageDto> GetLanguage(string name);
        Task<bool> TestKafka();
    }
}