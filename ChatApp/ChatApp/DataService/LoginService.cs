using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.DataService
{
    public interface LoginService
    {
        Task<UserLogin> AddUserAsync(UserLogin info);
        Task<UserLogin> GetUserAsync(string name);
        Task<List<UserLogin>> GetUsersAsync();
        Task<bool> UpdateUserStatusAsync(UserLogin info);
    }

}
