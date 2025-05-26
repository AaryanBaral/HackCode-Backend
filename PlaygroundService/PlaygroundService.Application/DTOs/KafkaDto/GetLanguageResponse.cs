

namespace PlaygroundService.Application.DTOs.KafkaDto
{
    public class GetLanguageResponse
    {
        public string? CorrelationID{ get; set; }
        public ReadLanguageDto? Data{ get; set; }

    }
}