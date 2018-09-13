using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackChat2.Controllers.model
{
    public class MessageModel
    {
        public int Id { get; set; }

        public int Sender { get; set; }

        public int Receiver { get; set; }

        public string Content { get; set; }

        public string DateTime { get; set; }
    }
}
