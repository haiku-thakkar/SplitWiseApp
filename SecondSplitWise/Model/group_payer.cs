using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Model
{
    public class group_payer
    {
        [Key]
        public int grouppayerID { get; set; }
        public int expenseID { get; set; }
        public int user_expense_id { get; set; }
        public decimal paid_share { get; set; }
    }
}
