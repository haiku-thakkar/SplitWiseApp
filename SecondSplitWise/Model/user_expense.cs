using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Model
{
    public class user_expense
    {
        [Key]
        public int userexpenseID { get; set; }

        public int? expenseID { get; set; }
        public expense expense { get; set; }

        public int? user_expense_id { get; set; }
        public user users { get; set; }

        public decimal paid_share { get; set; }

    }
}
