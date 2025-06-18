
namespace ConstrainService.Application.Constants
{
    public class KafkaTopics
    {
        public const string ValidateQuestionId = "validate-question-id";
        public const string ValidateQuestionIdResponse = "validate-questoin-id-response";
        public const string ValidateUserId = "validate-question-id";
        public const string ValidateUserIdResponse = "validate-questoin-id-response";
        public const string KafkaTest = "kafka-test";
        public static string[] GetKafkaTopics() => [
            ValidateQuestionId,
            KafkaTest,
            ValidateQuestionIdResponse,
            ValidateUserId,
            ValidateUserIdResponse
        ];
    }
}