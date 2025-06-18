using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Application.Constants
{
    public class KafkaTopics
    {
        public const string KafkaTest = "kafka-test";
        public const string ValidateUserId = "validate-user-id";
        public const string ValidateUserIdResponse = "validate-user-id-response";

        public const string ValidateUserRole = "validate-user-role";
        public const string ValidateUserRoleResponse = "validate-user-role-response";

        public static string[] GetKafkaTopics() => [
                KafkaTest,
                ValidateUserId,
                ValidateUserIdResponse,
                ValidateUserRole,
                ValidateUserRoleResponse
        ];
                
        
    }
}