using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondSplitWise.DataModel;
using SecondSplitWise.Model;
using SecondSplitWise.Repository;
using SecondSplitWise.Response;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecondSplitWise.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupController : Controller
    {

        IGroupRepository _GroupRepository;
        ILogger _Logger;

        public GroupController(IGroupRepository groupRepo,ILoggerFactory loggerFactory)
        {
            _GroupRepository = groupRepo;
            _Logger = loggerFactory.CreateLogger(nameof(GroupController));
        }

        [HttpGet("all/{id}")]
        [ProducesResponseType(typeof(List<GroupResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> Groups(int id)
        {
            try
            {
                var groups = await _GroupRepository.GetGroupsAsync(id);
                return Ok(groups);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }


        }

        [HttpGet("{id}",Name ="GetGroupRoute")]
        [ProducesResponseType(typeof(List<GroupResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> Group(int id)
        {
            try
            {
                var groups = await _GroupRepository.GetGroupAsync(id);
                return Ok(groups);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        [HttpGet("all/{Userid}/{Friendid}")]
        [ProducesResponseType(typeof(List<GroupResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> CommonGroups(int Userid, int Friendid)
        {

            try
            {
                var groups = await _GroupRepository.GetCommonGroupsAsync(Userid, Friendid);
                return Ok(groups);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        // POST api/group
        [HttpPost]
        [ProducesResponseType(typeof(ApiGeneralResponse), 201)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> CreateGroup([FromBody]groupModel group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiGeneralResponse { Status = false });
            }

            try
            {
                var newGroup = await _GroupRepository.InsertGroupAsync(group);
                if (newGroup == null)
                {
                    return BadRequest(new ApiGeneralResponse { Status = false });
                }
                return CreatedAtRoute("GetGroupRoute", new { id = newGroup.groupID },
                        new ApiGeneralResponse { Status = true, id = newGroup.groupID });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        // POST api/group/Groupid/Memberid
        [HttpPost("{Groupid}/{Memberid}")]
        [ProducesResponseType(typeof(members), 201)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> CreateGroupMember(int Groupid, int Memberid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiGeneralResponse { Status = false });
            }

            try
            {
                var newMember = await _GroupRepository.InsertGroupMemberAsync(Groupid, Memberid);
                if (newMember == null)
                {
                    return BadRequest(new ApiGeneralResponse { Status = false });
                }

                return CreatedAtAction("GetGroupMemberRoute", new { id = newMember.userID },
                          new ApiGeneralResponse { Status = true, id = (int)newMember.userID });

            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        // PUT api/group/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiGeneralResponse), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> UpdateGroup(int id, [FromBody]group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiGeneralResponse { Status = false });
            }

            try
            {
                var status = await _GroupRepository.UpdateGroupAsync(group);
                if (!status)
                {
                    return BadRequest(new ApiGeneralResponse { Status = false });
                }
                return Ok(new ApiGeneralResponse { Status = true, id = group.groupID });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiGeneralResponse), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            try
            {
                var status = await _GroupRepository.DeleteGroupAsync(id);
                if (!status)
                {
                    return BadRequest(new ApiGeneralResponse { Status = false });
                }
                return Ok(new ApiGeneralResponse { Status = true, id = id });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        // DELETE api/group/Groupid/Memberid
        [HttpDelete("{Groupid}/{Memberid}")]
        [ProducesResponseType(typeof(ApiGeneralResponse), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> DeleteUser(int Groupid, int Memberid)
        {
            try
            {
                var status = await _GroupRepository.DeleteGroupMemberAsync(Groupid, Memberid);
                if (!status)
                {
                    return BadRequest(new ApiGeneralResponse { Status = false });
                }
                return Ok(new ApiGeneralResponse { Status = true, id = Memberid });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }
    }
}
