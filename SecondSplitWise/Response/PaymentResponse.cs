using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Response
{
    public class PaymentResponse
    {
        public int ID { get; set; }
        public int PayreID { get; set; }
        public int ReceiverID { get; set; }
        public int groupID { get; set; }
        public decimal amount { get; set; }
    }
}
