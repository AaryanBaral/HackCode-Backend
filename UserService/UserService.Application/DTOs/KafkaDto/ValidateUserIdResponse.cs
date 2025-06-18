using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Application.DTOs.KafkaDto
{
    public class ValidateUserIdResponse
    {
        public bool IsValid { get; set; }
        public required string Message { get; set; }
        public required string CorrelationID { get; set; }
    }
    public class ValidateUserRoleResponse
    {
            public bool IsValid { get; set; }
        public required string Message { get; set; }
        public required string CorrelationID { get; set; }
    }
}