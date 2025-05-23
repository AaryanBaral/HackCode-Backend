using PlaygroundService.Domain.Entities;

namespace PlaygroundService.Application.Interfaces.Container
{
    public interface IContainerManager
    {
        Task CreateSandboxAsync(string userId, string language, CancellationToken cancellationToken);
        Task<SandboxSession> GetOrCreateSandboxAsync(string userId, string language, CancellationToken cancellationToken);
        Task TerminateSandboxAsync(string userId, CancellationToken cancellationToken);
        Task CleanupIdleSandboxesAsync(TimeSpan idleTimeout, CancellationToken cancellationToken);
    }
}