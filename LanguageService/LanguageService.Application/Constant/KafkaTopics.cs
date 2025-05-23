using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageService.Application.Constant
{
    public static class KafkaTopics
    {
        public const string GetLanguageByName = "get-language-by-name";
        public const string KafkaTest = "kafka-test";
        public static string[] GetKafkaTopics() => [
            GetLanguageByName,
            KafkaTest
        ];
    }


}