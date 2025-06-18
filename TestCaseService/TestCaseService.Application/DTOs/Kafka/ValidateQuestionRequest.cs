
namespace ConstrainService.Application.DTOs.Kafka
{
    public class ValidateQuestionRequest
    {
        public required string QuestionId { get; set; }
        public required string CorrelationID { get; set; }
    }
}