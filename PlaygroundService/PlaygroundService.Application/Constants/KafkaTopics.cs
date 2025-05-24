using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaygroundService.Application.Constants
{
    public static class KafkaTopics
    {
        public const string GetLanguageByName = "get-language-by-name";
        public const string GetLanguageByNameResponse = "get-language-by-name-response";
        public const string KafkaTest = "kafka-test";
        public static string[] GetKafkaTopics() => [
            GetLanguageByNameResponse,
            KafkaTest
        ];
    }
}