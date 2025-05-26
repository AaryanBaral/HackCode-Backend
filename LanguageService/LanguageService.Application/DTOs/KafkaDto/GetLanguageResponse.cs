using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageService.Application.DTOs.LanguageDto;

namespace LanguageService.Application.DTOs.KafkaDto
{
    public class GetLanguageResponse
    {
        public required string CorrelationID { get; set; }
        public ReadLanguageDto? Data { get; set; }
    }
}