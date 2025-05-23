using LanguageService.Application.DTOs.LanguageDto;
using LanguageService.Application.Interfaces;
using LanguageService.Application.Mapper;


namespace LanguageService.Application.Services
{
    public class LanguageServiceProvider(ILanguageRepository repository) : ILanguageService
    {
        private readonly ILanguageRepository _repository = repository;

        public async Task CreateLanguageAsync(AddLanguageDto addLanguageDto)
        {
            var language = addLanguageDto.ToLanguage();
            await _repository.CreateLanguage(language);

        }

        public async Task<ReadLanguageDto?> GetLanguageById(string id)
        {
            var language = await _repository.GetLanguageById(id);
            if (language is null) return null;
            return language.ToReadLanguage();
        }

        public async Task<ReadLanguageDto?> GetLanguageByName(string name)
        {
            var language = await _repository.GetLanguageByName(name);
            if (language is null) return null;
            return language.ToReadLanguage();
        }
        public async Task<List<ReadLanguageDto>?> GetAllLanguage()
        {
            var language = await _repository.GetLanguagesAsync();
            if (language is null) return null;
            return [.. language.Select(e => e.ToReadLanguage())];
        }
    }
}