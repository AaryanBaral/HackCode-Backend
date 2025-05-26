
using Docker.DotNet;

namespace PlaygroundService.Infrastructure.Configurations.Docker
{
    public interface IDockerClientFactory
    {
        DockerClient CreateClient();
    }
}