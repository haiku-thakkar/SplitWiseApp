using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackChat2.Hubs
{
    public class ChatHub : Hub 
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public async Task Send(string data,string conId)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("send", data);
            await Clients.Client(conId).SendAsync("send", data);
        }
        public async Task upData()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("upData", Context.ConnectionId);
        }
    }
}
