using ChatApp.Data;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.DataService
{
    public class SQLMessageService : MessageService 
    {
        private readonly ChatAppContext _context;
        private readonly ILogger _Logger;
        public SQLMessageService(ChatAppContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _Logger = loggerFactory.CreateLogger("MessageRepository");
        }
        public async Task<Messages> AddMsgAsync(Messages data)
        {
            _context.Add(data);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception exp)
            {
                _Logger.LogError($"Error in {nameof(AddMsgAsync)}: " + exp.Message);
            }
            return data;
        }


        public async Task<List<Messages>> GetMsgAsync()
        {
            return await _context.Messages.ToListAsync();
        }


        public async Task<bool> UpdateReadStatusAsync(Messages data)
        {
            _context.Messages.Attach(data);
            _context.Entry(data).State = EntityState.Modified;
            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(UpdateReadStatusAsync)}: " + exp.Message);
            }
            return false;
        }

    }
}
