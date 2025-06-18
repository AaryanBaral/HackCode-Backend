

namespace ConstrainService.Application.DTOs.Kafka
{
    public class ValidateQuestionResponse
    {
        public bool IsValid { get; set; }
        public required string CorrelationID { get; set; }
    }
}