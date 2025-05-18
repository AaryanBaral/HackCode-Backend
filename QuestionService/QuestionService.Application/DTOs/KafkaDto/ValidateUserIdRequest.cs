using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionService.Application.DTOs.KafkaDto
{
    public class ValidateUserIdRequest
    {
        
        public required string UserID { get; set; }
        public required string CorrelationID { get; set; }
    }
}