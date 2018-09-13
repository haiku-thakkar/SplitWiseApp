using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Model
{
    public class expense_member
    {
        [Key]
        public int expenseMemberID { get; set; }

        public int? expenseID { get; set; }
        public expense expense { get; set; }

        public int? commonmemberID { get; set; }
        public user user { get; set; }

        public decimal payableAmount { get; set; }
    }
}
