using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlaygroundService.Application.DTOs.KafkaDto;

namespace PlaygroundService.Application.Interfaces.LanguageGetter
{
    public interface ILanguageGetter
    {
        Task<GetLanguageResponse> GetLanguage(string language);
    }
}