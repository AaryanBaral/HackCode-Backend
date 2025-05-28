using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionService.Application.DTOs.KafkaDto
{
    public class QuestionDeleteRequest
    {
        
        public required string CorrelationID { get; set; }
        public required string QuestionId { get; set; }
    }
}