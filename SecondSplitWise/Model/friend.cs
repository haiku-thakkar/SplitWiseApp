using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Model
{
    public class friend
    {
        [Key]
        public int friendID { get; set; }

        public int userID { get; set; }
        public user user { get; set; }

        public int fID { get; set; }
        public user Friend { get; set; }
    }
}
