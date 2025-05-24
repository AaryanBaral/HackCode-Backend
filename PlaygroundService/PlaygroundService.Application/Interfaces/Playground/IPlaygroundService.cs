using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlaygroundService.Application.DTOs.KafkaDto;

namespace PlaygroundService.Application.Interfaces.Playground
{
    public interface IPlaygroundService
    {
        Task<GetLanguageResponse> GetLanguage(string name);
        Task<bool> TestKafka();
    }
}