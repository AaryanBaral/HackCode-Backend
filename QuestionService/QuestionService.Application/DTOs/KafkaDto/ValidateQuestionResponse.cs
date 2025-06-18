namespace QuestionService.Application.DTOs.KafkaDto
{
    public class ValidateQuestionResponse
    {
        public bool IsValid { get; set; }
        public required string CorrelationID { get; set; }
    }
}