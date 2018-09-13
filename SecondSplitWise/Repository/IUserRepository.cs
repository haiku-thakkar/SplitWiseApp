using SecondSplitWise.Model;
using SecondSplitWise.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Repository
{
   public interface IUserRepository
    {
        Task<List<user>> GetUsersAsync();
        Task<UserResponse> GetUserAsync(int id);

        Task<user> InsertUserAsync(user user);
        Task<bool> UpdateUserAsync(user user);
        Task<bool> DeleteUserAsync(int id);

        Task<user> LoginUserAsync(string email, string password);
    }
}
