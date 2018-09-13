using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondSplitWise.Repository;
using SecondSplitWise.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecondSplitWise.Controllers
{
    [Route("api/friend")]
    [ApiController]
    public class FriendController : Controller
    {
        IFriendRepository _FriendRepository;
        ILogger _Logger;

        public FriendController(IFriendRepository friendRepo,ILoggerFactory loggerFactory)
        {
            _FriendRepository = friendRepo;
            _Logger = loggerFactory.CreateLogger(nameof(FriendController));
        }

        // GET: api/<controller>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FriendResponse), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> GetFriend(int id)
        {
            try
            {
                var friends = await _FriendRepository.GetFriendAsync(id);
                return Ok(friends);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        // GET api/<controller>/5
        [HttpGet("all/{id}")]
        [ProducesResponseType(typeof(FriendResponse), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> GetFriends(int id)
        {
            try
            {
                var friends = await _FriendRepository.GetAllFriendsAsync(id);
                return Ok(friends);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        // POST api/<controller>
        [HttpPost("{Userid}/{Friendid}")]
        [ProducesResponseType(typeof(ApiGeneralResponse), 201)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> CreateFriend(int Userid, int Friendid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiGeneralResponse { Status = false });
            }

            try
            {
                var newUser = await _FriendRepository.InsertFriendAsync(Userid, Friendid);
                if (newUser == null)
                {
                    return BadRequest(new ApiGeneralResponse { Status = false });
                }
                return CreatedAtAction("GetFriendRoute", new { id = newUser.userID },
                       new ApiGeneralResponse { Status = true, id = newUser.userID });


            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }


        // DELETE api/<controller>/5
        [HttpDelete("{uid}/{fid}")]
        [ProducesResponseType(typeof(ApiGeneralResponse), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> Delete(int fid, int uid)
        {
            try
            {
                var status = await _FriendRepository.DeleteFriendAsync(fid, uid);
                if (!status)
                {
                    return BadRequest(new ApiGeneralResponse { Status = false });
                }
                return Ok(new ApiGeneralResponse { Status = true, id = fid });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }
    }
}
