using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaygroundService.Application.Constants
{
    public static class KafkaTopics
    {
        public const string GetLanguageByName = "get-language-by-name";
        public static string[] GetKafkaTopics() => [
            GetLanguageByName
        ];
    }
}