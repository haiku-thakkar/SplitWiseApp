using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.DataService
{
    public interface MessageService
    {
        Task<Messages> AddMsgAsync(Messages data);
        Task<List<Messages>> GetMsgAsync();
        Task<bool> UpdateReadStatusAsync(Messages data);
    }

}
