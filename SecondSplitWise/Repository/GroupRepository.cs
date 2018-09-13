using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecondSplitWise.DataModel;
using SecondSplitWise.DBContext;
using SecondSplitWise.Model;
using SecondSplitWise.Response;

namespace SecondSplitWise.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly SecondSplitWiseContext _Context;
        private readonly ILogger _Logger;

        public GroupRepository(SecondSplitWiseContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("GroupRepository");
        }

        public async Task<bool> DeleteGroupAsync(int id)
        {
            var group = await _Context.group.SingleOrDefaultAsync(g => g.groupID == id);
            _Context.Remove(group);
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (System.Exception exp)
            {
                _Logger.LogError($"Error in {nameof(DeleteGroupAsync)}: " + exp.Message);
            }
            return false;
        }

        public async Task<bool> DeleteGroupMemberAsync(int Groupid, int MembersID)
        {
            var gpMember = _Context.member.SingleOrDefault(c => c.groupID == Groupid && c.userID == MembersID);
            _Context.Remove(gpMember);
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (System.Exception exp)
            {
                _Logger.LogError($"Error in {nameof(DeleteGroupMemberAsync)}: " + exp.Message);
            }
            return false;
        }

        public async Task<List<GroupResponse>> GetCommonGroupsAsync(int Userid, int Friendid)
        {
            List<GroupResponse> groups = new List<GroupResponse>();
            var gpData = _Context.group.Where(c => c.members.Any(gm => gm.userID == Userid)).Include(c => c.members).ToList();
            var data = gpData.Where(c => c.members.Any(gm => gm.userID == Friendid)).ToList();
            for (var i = 0; i < data.Count; i++)
            {
                var group = new GroupResponse();
                group = await GetGroupAsync(data[i].groupID);
                groups.Add(group);
            }
            return groups;
        }

        public async Task<GroupResponse> GetGroupAsync(int id)
        {
            GroupResponse group = new GroupResponse();
            List<MemberResponse> members = new List<MemberResponse>();

            var groupData = _Context.group.SingleOrDefault(c => c.groupID == id);
            group.groupID = groupData.groupID;
            group.group_name = groupData.group_name;
            group.updated_at = groupData.created_at;
            group.creatorID = groupData.created_by;

            var name = _Context.user.SingleOrDefault(c => c.userID == groupData.created_by);
            group.created_by = name.first_name;

            var memberData = _Context.member.Where(c => c.groupID == id).ToList();
            for (var i = 0; i < memberData.Count; i++)
            {
                var member = _Context.user.SingleOrDefault(c => c.userID == memberData[i].userID);
                members.Add(new MemberResponse(member.userID, member.first_name));

            }
            group.members = members;

            return group;
        }

        public async Task<List<GroupResponse>> GetGroupsAsync(int id)
        {
            List<GroupResponse> groups = new List<GroupResponse>();
            var gpData = _Context.group.Where(c => c.members.Any(ap => ap.userID == id)).ToList();
            for (var i = 0; i < gpData.Count; i++)
            {
                var group = new GroupResponse();
                group = await GetGroupAsync(gpData[i].groupID);
                groups.Add(group);
            }
            return groups;
        }

        public async Task<group> InsertGroupAsync(groupModel group)
        {
            group gp = new group();
            gp.group_name = group.group_name;
            gp.created_at = group.created_at;
            gp.created_by = group.created_by;
            _Context.group.Add(gp);

            foreach (var member in group.members)
            {
                members gpMember = new members();
                gpMember.groupID = gp.groupID;
                gpMember.userID = member;
                _Context.member.Add(gpMember);
            }

            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (System.Exception exp)
            {
                _Logger.LogError($"Error in {nameof(InsertGroupAsync)}: " + exp.Message);
            }
            return gp;
        }

        public async Task<members> InsertGroupMemberAsync(int Groupid, int MembersID)
        {
            var member = _Context.member.SingleOrDefault(c => c.groupID == Groupid && c.userID == MembersID);

            if (member == null)
            {

                members newMember = new members
                {
                    groupID = Groupid,
                    userID = MembersID
                };

                _Context.member.Add(newMember);
                await _Context.SaveChangesAsync();
                return newMember;
            }
            else
            {
                _Context.member.Attach(member);
                await _Context.SaveChangesAsync();
                return member;
            }
        }

        public async Task<bool> UpdateGroupAsync(group group)
        {
            _Context.group.Attach(group);
            _Context.Entry(group).State = EntityState.Modified;
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(UpdateGroupAsync)}: " + exp.Message);
            }
            return false;
        }

    }
}
