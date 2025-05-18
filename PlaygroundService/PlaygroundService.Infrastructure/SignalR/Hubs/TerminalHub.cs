using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PlaygroundService.Infrastructure.SignalR.Hubs
{
    public class TerminalHub : Hub
    {
        public async Task RunCode()
        {

        }


        public async Task TerminalCommand()
        {

        }

        public override async Task OnConnectedAsync()
        {

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            
        }
    }
}