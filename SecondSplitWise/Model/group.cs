using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Model
{
    public class group
    {
        [Key]
        public int groupID { get; set; }
        public string group_name { get; set; }

        public int created_by { get; set; }
        public user user { get; set; }

        public DateTime created_at { get; set; }

        [ForeignKey("groupID")]
        public ICollection<members> members { get; set; }

        [ForeignKey("groupID")]
        public ICollection<expense> expenses { get; set; }

        [ForeignKey("groupID")]
        public ICollection<group_transactions> Transactions { get; set; }

        [ForeignKey("groupID")]
        public ICollection<payment> paymentdetails { get; set; }
    }
}
