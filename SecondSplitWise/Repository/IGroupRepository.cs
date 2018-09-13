using SecondSplitWise.DataModel;
using SecondSplitWise.Model;
using SecondSplitWise.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Repository
{
    public interface IGroupRepository
    {
        Task<List<GroupResponse>> GetGroupsAsync(int id);
        Task<GroupResponse> GetGroupAsync(int id);
        Task<List<GroupResponse>> GetCommonGroupsAsync(int Userid, int Friendid);

        Task<bool> UpdateGroupAsync(group group);
        Task<bool> DeleteGroupAsync(int id);
        Task<group> InsertGroupAsync(groupModel group);

        Task<members> InsertGroupMemberAsync(int Groupid, int MembersID);
        Task<bool> DeleteGroupMemberAsync(int Groupid, int MembersID);
    }
}
