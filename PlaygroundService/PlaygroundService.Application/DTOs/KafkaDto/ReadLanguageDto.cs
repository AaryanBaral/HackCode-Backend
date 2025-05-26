using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaygroundService.Application.DTOs.KafkaDto
{
    public class ReadLanguageDto
    {

        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string DockerImage { get; set; }
        public required string FileExtension { get; set; }
        public string? CompileCommand { get; set; }
        public required string ExecuteCommand { get; set; }
        public DateTime CreatedAt { get; set; }
        public required DateTime ModifiedAt { get; set; }
    }
}