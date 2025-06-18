using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Application.DTOs.KafkaDto
{
    public class ValidateUserIdRequest
    {

        public required string UserID { get; set; }
        public required string CorrelationID { get; set; }
    }

    public class ValidateUserRoleRequest
    {
        public required string UserId { get; set; }
        public required string CorrelationID { get; set; }
    }
}