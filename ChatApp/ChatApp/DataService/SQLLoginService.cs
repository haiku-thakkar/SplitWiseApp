using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChatApp.DataService
{
    public class SQLLoginService : LoginService
    {
        private readonly ChatAppContext _context;
        private readonly ILogger _Logger;


        public SQLLoginService(ChatAppContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _Logger = loggerFactory.CreateLogger("UserRepository");
        }

        public async Task<UserLogin> AddUserAsync(UserLogin info)
        {
            _context.Add(info);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception exp)
            {
                _Logger.LogError($"Error in {nameof(AddUserAsync)}: " + exp.Message);
            }
            return info;
        }


        public async Task<UserLogin> GetUserAsync(string name)
        {
            return await _context.UserLogin
                                 .SingleOrDefaultAsync(c => c.name == name);
        }


        public async Task<List<UserLogin>> GetUsersAsync()
        {
            return await _context.UserLogin.ToListAsync();
        }


        public async Task<bool> UpdateUserStatusAsync(UserLogin info)
        {


            _context.UserLogin.Attach(info);
            _context.Entry(info).State = EntityState.Modified;
            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(UpdateUserStatusAsync)}: " + exp.Message);
            }
            return false;
        }


    }
}

