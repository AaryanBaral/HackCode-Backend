using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionService.Application.DTOs.KafkaDto
{
    public class QuestionDeleteResponse
    {
        public required string CorrelationID { get; set; }
        public required bool IsDeleted { get; set; }
    }
}