using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Response
{
    public class ExpenseResponse
    {
        public int expenseID { get; set; }
        public string expenseName { get; set; }
        public string createdBy { get; set; }
        public int groupID { get; set; }
        public string group_name { get; set; }
        public DateTime created_at { get; set; }
        public List<ExpenseMemberResponse> payers { get; set; }
        public List<ExpenseMemberResponse> expenseMembers { get; set; }
    }
}
