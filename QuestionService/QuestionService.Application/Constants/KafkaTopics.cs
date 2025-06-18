using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaygroundService.Application.Constants
{
    public static class KafkaTopics
    {
        public const string DeleteQuestionRequest = "delete-question-request";
        public const string DeleteQuestionResponse = "delete-question-response";
        public const string ValidateQuestionId = "validate-question-id";
        public const string ValidateQuestionIdResponse = "validate-questoin-id-response";
        public const string KafkaTest = "kafka-test";
        public static string[] GetKafkaTopics() => [
            DeleteQuestionRequest,
            KafkaTest,
            DeleteQuestionResponse,
            ValidateQuestionId,
            ValidateQuestionIdResponse
        ];
    }
}