using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Model
{
    public class members
    {
        [Key]
        public int membersID { get; set; }

        public int? userID { get; set; }
        public user users { get; set; }

        public int? groupID { get; set; }
        public group group { get; set; }
    }
}
