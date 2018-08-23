using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class ChatHub:Hub
    {
        private ChatAppContext _service;
        private UserLogin data;

        public ChatHub(ChatAppContext service)
        {
            _service = service;
        }

        public Task Send(string userId, string senderid, string message, string sender)
        {
            return Clients.Clients(userId, senderid).SendAsync("send", message, sender);
        }

        public void setconnectid(string name)
        {
            data = _service.UserLogin.SingleOrDefault(c => c.name == name);
            data.ConnectionID = Context.ConnectionId;
            _service.UserLogin.Attach(data);
            _service.Entry(data).State = EntityState.Modified;
            _service.SaveChanges();
        }

        public void setstatus(string name)
        {
            data = _service.UserLogin.SingleOrDefault(c => c.name == name);
            data.isConnect = "1";
            _service.UserLogin.Attach(data);
            _service.Entry(data).State = EntityState.Modified;
            _service.SaveChanges();
        }


    }
}
