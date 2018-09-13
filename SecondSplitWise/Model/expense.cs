using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Model
{
    public class expense
    {
        [Key]
        public int expenseID { get; set; }
        public string expenseName { get; set; }

        public int created_by { get; set; }
        public user user { get; set; }

        public DateTime created_at { get; set; }

        public int? groupID { get; set; }
        public group group { get; set; }

        public int paymentID { get; set; }
        public payment payments { get; set; }

        [ForeignKey("expenseID")]
        public ICollection<user_expense> user_Expenses { get; set; }

        [ForeignKey("expenseID")]
        public ICollection<expense_member> expense_Members { get; set; }
    }
}
