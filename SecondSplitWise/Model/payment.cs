using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Model
{
    public class payment
    {
        [Key]
        public int paymentID { get; set; }

        public int? payerID { get; set; }
        public user payer { get; set; }

        public int? commonmemberID { get; set; }
        public user commonmember { get; set; }

        public int? groupID { get; set; }
        public group groupsID { get; set; }

        public decimal payment_amount { get; set; }
    }
}
