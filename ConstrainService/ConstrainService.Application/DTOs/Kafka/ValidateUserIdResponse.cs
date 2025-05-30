using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstrainService.Application.DTOs.Kafka
{
    public class ValidateUserIdResponse
    {
        public bool IsValid { get; set; }
        public required string Message { get; set; }
        public required string CorrelationID { get; set; }
    }
}