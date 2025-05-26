using LanguageService.Domain.Entities;

namespace LanguageService.Application.Interfaces
{
    public interface ILanguageRepository
    {
        Task CreateLanguage(Language language);
        Task<Language?> GetLanguageById(string id);
        Task<List<Language>?> GetLanguagesAsync();
        Task<Language?> GetLanguageByName(string name);
    }
}