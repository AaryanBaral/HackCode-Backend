using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCaseService.Application.Constants
{
    public class KafkaTopics
    {
        public const string ValidateQuestionId = "validate-question-id";
        public const string ValidateQuestionIdResponse = "validate-questoin-id-response";
        public const string KafkaTest = "kafka-test";
        public static string[] GetKafkaTopics() => [
            KafkaTest,
            ValidateQuestionId,
            ValidateQuestionIdResponse
        ];
    }
}