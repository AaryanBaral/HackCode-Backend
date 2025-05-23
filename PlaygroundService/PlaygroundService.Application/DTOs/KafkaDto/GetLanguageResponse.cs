

namespace PlaygroundService.Application.DTOs.KafkaDto
{
    public class GetLanguageResponse
    {
        public required string CorrelationID{ get; set; }
        public required ReadLanguageDto Language{ get; set; }

    }
}