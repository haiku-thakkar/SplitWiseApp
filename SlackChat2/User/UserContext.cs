using Microsoft.EntityFrameworkCore;
using SlackChat2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackChat2.User
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Message> Message { get; set; }
    }
}
