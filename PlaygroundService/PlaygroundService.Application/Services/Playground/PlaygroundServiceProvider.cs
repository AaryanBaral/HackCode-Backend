using PlaygroundService.Application.DTOs.KafkaDto;
using PlaygroundService.Application.Interfaces.LanguageGetter;
using PlaygroundService.Application.Interfaces.Playground;

namespace PlaygroundService.Application.Services.Playground;

public class PlaygroundServiceProvider(ILanguageGetter languageGetter):IPlaygroundService
{
    private readonly ILanguageGetter _languageGetter = languageGetter;

    public async Task<ReadLanguageDto> GetLanguage(string name)
    {
        var language = await _languageGetter.GetLanguage(name);
        return language;
    }

    public async Task<bool> TestKafka()
    {
        await _languageGetter.KafkaTest();
        return true;
    }
    
}