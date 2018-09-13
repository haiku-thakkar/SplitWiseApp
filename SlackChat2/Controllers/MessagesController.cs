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
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly IMapper mapper;
        private readonly UserContext _context;
        public MessagesController(IMapper mapper, UserContext context)
        {
            this._context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MessageModel>> GetMessage(MessageModel messageResource)
        {
            var message = await _context.Message.ToListAsync();
            return mapper.Map<IEnumerable<Message>, IEnumerable<MessageModel>>(message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] MessageModel messageModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var message = mapper.Map<MessageModel, Message>(messageModel);
            _context.Message.Add(message);
            await _context.SaveChangesAsync();
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id,[FromBody] MessageModel messageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var message = await _context.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            mapper.Map<MessageModel, Message>(messageModel, message);
            await _context.SaveChangesAsync();
            return Ok(message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await _context.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            var messageModel = mapper.Map<Message, MessageModel>(message);
            return Ok(messageModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Message.FindAsync(id);
            if (message == null)
                return NotFound();
            _context.Remove(message);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
    }
}
