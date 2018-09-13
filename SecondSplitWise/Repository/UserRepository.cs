using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecondSplitWise.DBContext;
using SecondSplitWise.Model;
using SecondSplitWise.Response;

namespace SecondSplitWise.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SecondSplitWiseContext _Context;
        private readonly ILogger _Logger;

        public UserRepository(SecondSplitWiseContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("UserRepository");
        }

        public async Task<UserResponse> GetUserAsync(int id)
        {
            var userdata = await _Context.user.SingleOrDefaultAsync(c => c.userID == id);
            var user = new UserResponse();
            user.UserId = userdata.userID;
            user.first_name = userdata.first_name;
            user.Email = userdata.email;
            user.Password = userdata.password;
            return user;
        }

        public async Task<List<user>> GetUsersAsync()
        {
            return await _Context.user.ToListAsync();
        }

        public async Task<user> InsertUserAsync(user user)
        {
            _Context.Add(user);
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (System.Exception exp)
            {
                _Logger.LogError($"Error in {nameof(InsertUserAsync)}:" + exp.Message);
            }
            return user;
        }

        public async Task<user> LoginUserAsync(string email, string password)
        {
            List<user> users = await _Context.user.ToListAsync();
            var user = users.SingleOrDefault(c => c.email == email && c.password == password);
            return user;
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _Context.user.SingleOrDefaultAsync(c => c.userID == id);
            _Context.Remove(user);
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (System.Exception exp)
            {
                _Logger.LogError($"Error in {nameof(DeleteUserAsync)}: " + exp.Message);
            }
            return false;
        }


        public async Task<bool> UpdateUserAsync(user user)
        {
            _Context.user.Attach(user);
            _Context.Entry(user).State = EntityState.Modified;
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(UpdateUserAsync)}: " + exp.Message);
            }
            return false;
        }
    }
}
