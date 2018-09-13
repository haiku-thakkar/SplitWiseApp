using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecondSplitWise.DBContext;
using SecondSplitWise.Model;
using SecondSplitWise.Repository;
using SecondSplitWise.Response;

namespace SecondSplitWise.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class usersController : Controller
    {
        IUserRepository _userRepository;
        ILogger _Logger;

        public usersController(IUserRepository userRepo, ILoggerFactory loggerFactory)
        {
            _userRepository = userRepo;
            _Logger = loggerFactory.CreateLogger(nameof(usersController));
        }

        // GET: api/users
        [HttpGet]
        [ProducesResponseType(typeof(List<user>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]

        public async Task<ActionResult> Users()
        {
            try
            {
                var users = await _userRepository.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // GET: api/users/5
        [HttpGet("{id}", Name = "GetUserRoute")]

        [ProducesResponseType(typeof(user), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> Users(int id)
        {
            try
            {
                var user = await _userRepository.GetUserAsync(id);
                return Ok(user);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }
            try
            {
                var newUser = await _userRepository.InsertUserAsync(user);
                if (newUser == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return CreatedAtRoute("GetUserRoute", new { id = newUser.userID },
                    new ApiResponse { Status = true, user = newUser });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }

        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            try
            {
                var status = await _userRepository.UpdateUserAsync(user);
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true, user = user });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }



        // DELETE: api/users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                var status = await _userRepository.DeleteUserAsync(id);
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }
    }
}