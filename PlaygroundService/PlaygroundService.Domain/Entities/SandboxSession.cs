using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaygroundService.Domain.Entities
{
    public class SandboxSession
    {
        public string ContainerId { get; set; } = string.Empty;
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;
        public string Language { get; set; } = string.Empty; // Track language for session
    }
}