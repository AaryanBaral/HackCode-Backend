namespace ConstrainService.Application.DTOs.Kafka
{
    public class ValidateUserIdRequest
    {
        public bool IsValid { get; set; }
        public required string CorrelationID { get; set; }
    }
}