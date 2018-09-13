using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Response
{
    public class GroupTransactionResponse
    {
        public int ID { get; set; }
        public MemberResponse payer { get; set; }
        public MemberResponse receiver { get; set; }
        public int? groupID { get; set; }
        public string groupName { get; set; }
        public decimal paid_share { get; set; }
        public DateTime created_at { get; set; }
    }
}
