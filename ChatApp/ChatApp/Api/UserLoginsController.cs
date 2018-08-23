using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatApp.Models;
using ChatApp.Data;
using Microsoft.Extensions.Logging;
using ChatApp.DataService;

namespace ChatApp
{
    [Produces("application/json")]
    [Route("api/login")]
    public class UserLoginsController : Controller
    {
        private LoginService _logindata;
        private ILogger _Logger;

        public UserLoginsController(LoginService logindata,ILoggerFactory loggerFactory)
        {
            _logindata = logindata;
            _Logger = loggerFactory.CreateLogger(nameof(UserLoginsController));
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<UserLogin>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Employees()
        {
            try
            {
                var users = await _logindata.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(UserLogin), 200)]
        public async Task<ActionResult> GetUser(string name)
        {
            try
            {
                var user = await _logindata.GetUserAsync(name);
                return Ok(user);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> AddUser([FromBody]UserLogin info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            try
            {
                var newuser = await _logindata.AddUserAsync(info);
                if (newuser == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return CreatedAtRoute("GetUserRoute", new { id = newuser.id }, newuser);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> UpdateUser(int id, [FromBody]UserLogin info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            try
            {
                var status = await _logindata.UpdateUserStatusAsync(info);
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true, User = info });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }
    }
}