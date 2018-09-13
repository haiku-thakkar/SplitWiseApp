using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Model
{
    public class single_payer
    {
        [Key]
        public int singlepayerID { get; set; }
        public int expenseID { get; set; }
        public int user_expense_id { get; set; }
        public decimal paid_share { get; set; }
    }
}
