using Docker.DotNet.Models;
using Microsoft.Extensions.Logging;
using PlaygroundService.Application.Interfaces.Command;
using PlaygroundService.Application.Interfaces.LanguageGetter;
using PlaygroundService.Infrastructure.Configurations.Docker;

namespace PlaygroundService.Infrastructure.Command
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IDockerClientFactory _dockerClientFactory;
        private readonly ILanguageGetter _languageGetter;
        private readonly ILogger<CommandExecutor> _logger;

        public CommandExecutor(
            IDockerClientFactory dockerClientFactory,
            ILogger<CommandExecutor> logger,
            ILanguageGetter languageGetter)
        {
            _dockerClientFactory = dockerClientFactory;
            _logger = logger;
            _languageGetter = languageGetter;
        }

        public async Task WriteCodeAsync(string containerId, string fileName, string code, CancellationToken cancellationToken)
        {
            var command = $"echo '{code.Replace("'", "\\'")}' > {fileName}";
            await ExecuteCommandAsync(containerId, command, cancellationToken);
            _logger.LogInformation("Wrote code to {FileName} in container {ContainerId}.", fileName, containerId);
        }

        public async Task<string> ExecuteCodeAsync(string containerId, string language, string fileName, CancellationToken cancellationToken)
        {
            var languageResponse = await _languageGetter.GetLanguage(language);
            var outputBuilder = new System.Text.StringBuilder();

            // Compile if required
            if (!string.IsNullOrEmpty(languageResponse.Data.CompileCommand))
            {
                var compileCommand = languageResponse.Data.CompileCommand.Replace("{file}", fileName);
                var compileOutput = await ExecuteCommandAsync(containerId, compileCommand, cancellationToken);
                outputBuilder.AppendLine(compileOutput);
                if (compileOutput.Contains("error", StringComparison.OrdinalIgnoreCase))
                {
                    return outputBuilder.ToString(); // Return compilation errors
                }
            }

            // Execute
            var executeCommand = languageResponse.Data.ExecuteCommand.Replace("{file}", fileName);
            var executeOutput = await ExecuteCommandAsync(containerId, executeCommand, cancellationToken);
            outputBuilder.AppendLine(executeOutput);

            _logger.LogInformation("Executed code {FileName} in container {ContainerId} for language {Language}.", fileName, containerId, language);
            return outputBuilder.ToString();
        }

        public async Task<string>   ExecuteCommandAsync(string containerId, string command, CancellationToken cancellationToken)
        {
            using var client = _dockerClientFactory.CreateClient();
            var containerInspect = await client.Containers.InspectContainerAsync(containerId, cancellationToken);
            if (!containerInspect.State.Running)
            {
                _logger.LogError("Container {ContainerId} is not running.", containerId);
                throw new InvalidOperationException($"Container {containerId} is not running.");
            }
                var tempContainer = await client.Containers.CreateContainerAsync(new CreateContainerParameters
                {
                    Image = containerInspect.Config.Image,
                    Cmd = new[] { "/bin/bash", "-c", command },
                    HostConfig = new HostConfig
                    {
                        AutoRemove = true, // Automatically remove the container when it exits
                        Binds = containerInspect.HostConfig.Binds // Inherit volume mounts to access same filesystem
                    },
                    WorkingDir = containerInspect.Config.WorkingDir, // Match working directory
                    User = containerInspect.Config.User // Match user for consistent permissions
                }, cancellationToken);

                // Step 3: Attach to the temporary container to capture output
                using var stream = await client.Containers.AttachContainerAsync(
                    tempContainer.ID,
                    false, // Tty = false for multiplexed output
                    new ContainerAttachParameters
                    {
                        Stream = true,
                        Stdout = true,
                        Stderr = true
                    },
                    cancellationToken);

                // Step 4: Start the temporary container
                await client.Containers.StartContainerAsync(tempContainer.ID, new ContainerStartParameters(), cancellationToken);

                // Step 5: Read the output
                var (stdout, stderr) = await stream.ReadOutputToEndAsync(cancellationToken);

                _logger.LogDebug("Executed command '{Command}' in temporary container {TempContainerId} for target container {ContainerId}.", command, tempContainer.ID, containerId);
                return stdout + stderr;

        }
    }
}