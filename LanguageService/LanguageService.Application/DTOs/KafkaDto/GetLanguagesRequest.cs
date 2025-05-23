using LanguageService.Application.DTOs.LanguageDto;

namespace LanguageService.Application.DTOs.KafkaDto
{
    public class GetLanguageRequest
    {
        public required string CorrelationID { get; set; }
        public required string Name { get; set; }
    }
}