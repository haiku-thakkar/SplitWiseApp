using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Model
{
    public class user
    {
        [Key]
        public int userID { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        [ForeignKey("userID")]
        public ICollection<members> members { get; set; }

        [ForeignKey("created_by")]
        public ICollection<expense> expenses { get; set; }

        [ForeignKey("created_by")]
        public ICollection<group> groups { get; set; }

        [ForeignKey("user_expense_id")]
        public ICollection<user_expense> user_Expense_data { get; set; }

        [ForeignKey("commonmemberID")]
        public ICollection<expense_member> expense_Members { get; set; }

        [InverseProperty("user")]
        public List<friend> Users { get; set; }

        [InverseProperty("Friend")]
        public List<friend> friends { get; set; }

        [InverseProperty("payer")]
        public List<payment> payers { get; set; }

        [InverseProperty("commonMember")]
        public List<payment> commonmembers { get; set; }

        [InverseProperty("grouptransPayer")]
        public List<group_transactions> group_Transactions_Payer { get; set; }

        [InverseProperty("grouptransReceiver")]
        public List<group_transactions> group_Transactions_Receiver { get; set; }

    }
}
