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
    public class FriendRepository : IFriendRepository
    {
        private readonly SecondSplitWiseContext _Context;
        private readonly ILogger _Logger;

        public FriendRepository(SecondSplitWiseContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("FriendRepository");
        }

        public async Task<bool> DeleteFriendAsync(int uid, int fid)
        {
            var data = _Context.friend.SingleOrDefault(c => c.userID == uid && c.friendID == fid);
            _Context.Remove(data);

            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (System.Exception exp)
            {
                _Logger.LogError($"Error in {nameof(DeleteFriendAsync)}: " + exp.Message);
            }
            return false;
        }

        public async Task<List<FriendResponse>> GetAllFriendsAsync(int id)
        {
            return await _Context.friend
                     .Where(c => c.userID == id)
                    .Select(c => new FriendResponse()
                    {

                        userID = c.friendID,
                        first_name = c.Friend.first_name,
                        email = c.Friend.email

                    }).ToListAsync();
        }

        public async Task<FriendResponse> GetFriendAsync(int id)
        {
            var userData = await _Context.user.SingleOrDefaultAsync(c => c.userID == id);
            var user = new FriendResponse();
            user.userID = userData.userID;
            user.first_name = userData.first_name;
            user.email = userData.email;
            return user;
        }

        public async Task<friend> InsertFriendAsync(int Userid, int Friendid)
        {
            var member = _Context.friend.SingleOrDefault(c => c.userID == Userid && c.friendID == Friendid);

            if (member == null)
            {
                friend newFriend = new friend
                {
                    userID = Userid,
                    friendID = Friendid
                };

                _Context.friend.Add(newFriend);
                await _Context.SaveChangesAsync();
                return newFriend;
            }

            else
            {
                _Context.friend.Attach(member);
                await _Context.SaveChangesAsync();
                return member;
            }
        }
    }
}
