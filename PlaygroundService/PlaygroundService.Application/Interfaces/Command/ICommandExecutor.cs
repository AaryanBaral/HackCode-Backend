using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaygroundService.Application.Interfaces.Command
{
    public interface ICommandExecutor
    {
        Task WriteCodeAsync(string containerId, string fileName, string code, CancellationToken cancellationToken);
        Task<string> ExecuteCodeAsync(string containerId, string language, string fileName, CancellationToken cancellationToken);
        Task<string> ExecuteCommandAsync(string containerId, string command, CancellationToken cancellationToken);
    }
}