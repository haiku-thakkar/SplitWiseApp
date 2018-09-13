using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SlackChat2.Models
{
    [Table("Users")]
    public class Users
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(15)]
        public string Password { get; set; }

        public string ConnectionId { get; set; }

        public bool Status { get; set; }
    }
}
