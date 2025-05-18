using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionService.Application.DTOs.KafkaDto
{
    public class ValidateUserIDResponse
    {
        public bool IsValid { get; set; }
        public required string Message { get; set; }
        public required string CorrelationID { get; set; }
    }
}