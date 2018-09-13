using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Model
{
    public class group_transactions
    {
        [Key]
        public int grouptransactionID { get; set; }

        public int? grouptransPayerID { get; set; }
        public user grouptransPayer { get; set; }

        public int? grouptransReceiverID { get; set; }
        public user grouptransReceiver { get; set; }

        public int? groupID { get; set; }
        public group groupsID { get; set; }

        public decimal paid_share { get; set; }

        public DateTime created_at { get; set; }
    }
}
