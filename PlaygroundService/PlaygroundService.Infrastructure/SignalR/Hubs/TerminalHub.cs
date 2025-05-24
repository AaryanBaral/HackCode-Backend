using Microsoft.AspNetCore.SignalR;
using PlaygroundService.Application.Interfaces.Command;
using PlaygroundService.Application.Interfaces.Container;
using PlaygroundService.Application.Interfaces.LanguageGetter;

namespace PlaygroundService.Infrastructure.SignalR.Hubs
{
    public class TerminalHub(
    IContainerManager containerManager,
    ILanguageGetter languageGetter,
    ICommandExecutor commandExecutor) : Hub
    {
        private readonly IContainerManager _containerManager = containerManager;
        private readonly ILanguageGetter _languageGetter = languageGetter;
        private readonly ICommandExecutor _commandExecutor = commandExecutor;

        public async Task RunCode(string language, string code)
        {
            var userId = Context.UserIdentifier ?? Context.ConnectionId;
            var languageResponse = await _languageGetter.GetLanguage(language);
            var session = await _containerManager.GetOrCreateSandboxAsync(userId, language, Context.ConnectionAborted);
            var fileName = $"main{languageResponse.Data.FileExtension}";
            await _commandExecutor.WriteCodeAsync(session.ContainerId, fileName, code, Context.ConnectionAborted);
            var output = await _commandExecutor.ExecuteCodeAsync(session.ContainerId, language, fileName, Context.ConnectionAborted);
            await Clients.Caller.SendAsync("ReceiveOutput", output);
        }


        public async Task TerminalCommand(string command)
        {
            var userId = Context.UserIdentifier ?? Context.ConnectionId;
            var session = await _containerManager.GetOrCreateSandboxAsync(userId, "python", Context.ConnectionAborted); // Default to Python for commands
            var output = await _commandExecutor.ExecuteCommandAsync(session.ContainerId, command, Context.ConnectionAborted);
            await Clients.Caller.SendAsync("ReceiveOutput", output);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier ?? Context.ConnectionId;
            await _containerManager.CreateSandboxAsync(userId, "python", Context.ConnectionAborted); // Default to Python
            await Clients.Caller.SendAsync("ReceiveOutput", "Connected to sandbox.\n");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier ?? Context.ConnectionId;
            await _containerManager.TerminateSandboxAsync(userId, CancellationToken.None);
            await base.OnDisconnectedAsync(exception);
        }
    }
}