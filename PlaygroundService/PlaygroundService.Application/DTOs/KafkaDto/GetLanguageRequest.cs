using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaygroundService.Application.DTOs.KafkaDto
{
    public class GetLanguageRequest
    {
        public required string CorrelationID { get; set; }
        public required string Name { get; set; }
    }
}