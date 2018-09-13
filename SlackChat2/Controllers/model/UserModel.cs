using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackChat2.Controllers.model
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string ConnectionId { get; set; }

        public bool Status { get; set; }
    }
}
