using Docker.DotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PlaygroundService.Infrastructure.Configurations.Docker
{
    public class DockerClientFactory:IDockerClientFactory
    {
      private readonly IConfiguration _configuration;
        private readonly ILogger<DockerClientFactory> _logger;

        public DockerClientFactory(IConfiguration configuration, ILogger<DockerClientFactory> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public DockerClient CreateClient()
        {
            try
            {
                var dockerUri = _configuration.GetValue<string>("Docker:Uri", "unix:///var/run/docker.sock");
                var client = new DockerClientConfiguration(new Uri(dockerUri)).CreateClient();
                _logger.LogInformation("Docker client initialized with URI {DockerUri}.", dockerUri);
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize Docker client.");
                throw;
            }
        }
    }
}