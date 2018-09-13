using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlackChat2.Controllers.model;
using SlackChat2.Models;
using SlackChat2.User;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SlackChat2.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly UserContext _context;
        private readonly IMapper mapper;

        public UserController(IMapper mapper,UserContext context)
        {
            this._context = context;
            this.mapper = mapper;
        }

        [HttpGet]
       public async Task<IEnumerable<UserModel>>GetUsers(UserModel userModel)
        {
            var users = await _context.Users.ToListAsync();
            return mapper.Map<IEnumerable<Users>, IEnumerable<UserModel>>(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsers([FromBody] UserModel usersResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var users = mapper.Map<UserModel, Users>(usersResource);
            _context.Users.Add(users);
            await _context.SaveChangesAsync();
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsers(int id, [FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var users = await _context.Users.FindAsync(id);
            if (users == null)
                return NotFound();
            mapper.Map<UserModel, Users>(userModel, users);
            await _context.SaveChangesAsync();
            return Ok(users);
        }
         
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
                return NotFound();
            var userModel = mapper.Map<Users, UserModel>(users);
            return Ok(userModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
                return NotFound();
            _context.Remove(users);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
    }
}
