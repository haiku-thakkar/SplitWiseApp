using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.DataModel
{
    public class paymentModel
    {
        public int paymentID { get; set; }
        public int commonmemberID { get; set; }
        public int? groupID { get; set; }
        public decimal payment_amount { get; set; }
    }
}
