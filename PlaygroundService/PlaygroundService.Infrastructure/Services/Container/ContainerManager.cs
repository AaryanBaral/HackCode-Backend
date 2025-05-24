using System.Collections.Concurrent;
using Docker.DotNet.Models;
using PlaygroundService.Application.Interfaces.Container;
using PlaygroundService.Application.Interfaces.LanguageGetter;
using PlaygroundService.Domain.Entities;
using PlaygroundService.Infrastructure.Configurations.Docker;

namespace PlaygroundService.Infrastructure.Services.Container
{
    public class ContainerManager(
        IDockerClientFactory dockerClientFactory, ILanguageGetter languageGetter) : IContainerManager
    {
        private readonly IDockerClientFactory _dockerClientFactory = dockerClientFactory;
        private readonly ILanguageGetter _languageGetter = languageGetter;
        private readonly ConcurrentDictionary<string, SandboxSession> _sessions = new ConcurrentDictionary<string, SandboxSession>();

        public async Task CreateSandboxAsync(string userId, string language, CancellationToken cancellationToken)
        {
            var sessionKey = $"{userId}:{language}";
            if (_sessions.ContainsKey(sessionKey))
            {
                Console.WriteLine($"Sandbox already exists for user {userId} and language {language}.");
                return;
            }

            var containerId = await CreateContainerAsync(language, cancellationToken);
            _sessions[sessionKey] = new SandboxSession
            {
                ContainerId = containerId,
                LastActivity = DateTime.UtcNow,
                Language = language
            };
            Console.WriteLine($"Created sandbox {containerId} for user {userId} and language {language}.");
        }

        public async Task<SandboxSession> GetOrCreateSandboxAsync(string userId, string language, CancellationToken cancellationToken)
        {
            var sessionKey = $"{userId}:{language}";
            if (_sessions.TryGetValue(sessionKey, out var session))
            {
                session.LastActivity = DateTime.UtcNow;
                return session;
            }

            await CreateSandboxAsync(userId, language, cancellationToken);
            return _sessions[sessionKey];
        }

        public async Task TerminateSandboxAsync(string userId, CancellationToken cancellationToken)
        {
            var userSessions = _sessions.Where(kvp => kvp.Key.StartsWith($"{userId}:")).ToList();
            foreach (var session in userSessions)
            {
                if (_sessions.TryRemove(session.Key, out var removedSession))
                {
                    using var client = _dockerClientFactory.CreateClient();
                    Console.WriteLine("Terminating sandbox {ContainerId} for user {UserId}.", removedSession.ContainerId, userId);
                    await client.Containers.StopContainerAsync(removedSession.ContainerId, new ContainerStopParameters(), cancellationToken);
                    await client.Containers.RemoveContainerAsync(removedSession.ContainerId, new ContainerRemoveParameters(), cancellationToken);
                    Console.WriteLine("Terminated sandbox {ContainerId} for user {UserId}.", removedSession.ContainerId, userId);


                }
            }
        }

        public async Task CleanupIdleSandboxesAsync(TimeSpan idleTimeout, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            foreach (var session in _sessions)
            {
                if (now - session.Value.LastActivity > idleTimeout)
                {
                    await TerminateSandboxAsync(session.Key.Split(':')[0], cancellationToken);
                    Console.WriteLine("Cleaned up idle sandbox for user {UserId} and language {Language}.", session.Key.Split(':')[0], session.Value.Language);
                }
            }
        }

        private async Task<string> CreateContainerAsync(string language, CancellationToken cancellationToken)
        {
            using var client = _dockerClientFactory.CreateClient();
            var languageResponse = await _languageGetter.GetLanguage(language);
            var response = await client.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = languageResponse.Data.DockerImage,
                Cmd = new[] { "/bin/bash" },
                HostConfig = new HostConfig
                {
                    Memory = 512 * 1024 * 1024,
                    CPUCount = 1,
                    NetworkMode = "none"
                    // TODO: Add AppArmor or seccomp profiles for enhanced security
                }
            }, cancellationToken);

            await client.Containers.StartContainerAsync(response.ID, new ContainerStartParameters(), cancellationToken);
            Console.WriteLine($"Created and started container {response.ID} for language {language}.");
            return response.ID;
        }

    }
}