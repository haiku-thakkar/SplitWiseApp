using SecondSplitWise.Model;
using SecondSplitWise.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Repository
{
    public interface IFriendRepository
    {
        Task<FriendResponse> GetFriendAsync(int id);
        Task<List<FriendResponse>> GetAllFriendsAsync(int id);
        Task<friend> InsertFriendAsync(int Userid, int Friendid);
        Task<bool> DeleteFriendAsync(int uid, int fid);
    }
}
