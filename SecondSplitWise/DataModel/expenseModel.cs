using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.DataModel
{
    public class expenseModel
    {
        public string expenseName { get; set; }
        public int created_by { get; set; }
        public DateTime created_at { get; set; }
        public int? groupID { get; set; }
        public List<user_expenseModel> payer { get; set; }
        public List<user_expenseModel> commonmember { get; set; }
        public List<paymentModel> paymentModels { get; set; }
    }
}
