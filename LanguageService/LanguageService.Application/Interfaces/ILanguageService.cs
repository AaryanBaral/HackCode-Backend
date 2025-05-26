using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageService.Application.DTOs.LanguageDto;

namespace LanguageService.Application.Interfaces
{
    public interface ILanguageService
    {
        Task CreateLanguageAsync(AddLanguageDto addLanguageDto);
        Task<ReadLanguageDto?> GetLanguageById(string id);
        Task<ReadLanguageDto?> GetLanguageByName(string name);
        Task<List<ReadLanguageDto>?> GetAllLanguage();

    }
}