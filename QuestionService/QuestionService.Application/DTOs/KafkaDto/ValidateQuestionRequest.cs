using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionService.Application.DTOs.KafkaDto
{
    public class ValidateQuestionRequest
    {
        public required string QuestionId { get; set; }
        public required string CorrelationID { get; set; }
    }
}