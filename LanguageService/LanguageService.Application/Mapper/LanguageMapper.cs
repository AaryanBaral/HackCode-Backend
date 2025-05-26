using LanguageService.Application.DTOs.LanguageDto;
using LanguageService.Domain.Entities;

namespace LanguageService.Application.Mapper
{
    public static class LanguageMapper
    {
        public static Language ToLanguage(this AddLanguageDto addLanguageDto)
        {
            return new Language()
            {
                Name = addLanguageDto.Name,
                DockerImage = addLanguageDto.DockerImage,
                FileExtension = addLanguageDto.FileExtension,
                CompileCommand = addLanguageDto.CompileCommand,
                ExecuteCommand = addLanguageDto.ExecuteCommand,
                ModifiedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
        }
        public static ReadLanguageDto ToReadLanguage(this Language language)
        {
            return new ReadLanguageDto()
            {
                Id = language.Id,
                Name = language.Name,
                DockerImage = language.DockerImage,
                FileExtension = language.FileExtension,
                CompileCommand = language.CompileCommand,
                ExecuteCommand = language.ExecuteCommand,
                ModifiedAt = language.ModifiedAt,
                CreatedAt = language.CreatedAt
            };
        }
    }
}