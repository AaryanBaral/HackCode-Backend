
using PlaygroundService.Application.DTOs.KafkaDto;

namespace PlaygroundService.Application.Interfaces.LanguageGetter
{
    public interface ILanguageGetter
    {
        Task<GetLanguageResponse> GetLanguage(string language);
        Task<bool> KafkaTest();
    }
}