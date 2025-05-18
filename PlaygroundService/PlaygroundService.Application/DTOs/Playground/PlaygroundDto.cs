using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaygroundService.Application.DTOs.PlaygroundDtos
{
    public class AddPlaygroundDto
    {
        public required string UserId { get; set; } 
        public required string Code { get; set; }
        public required string Language { get; set; }
    }
}