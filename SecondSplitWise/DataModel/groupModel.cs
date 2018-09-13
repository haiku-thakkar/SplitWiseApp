using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.DataModel
{
    public class groupModel
    {
        public int groupID { get; set; }
        public string group_name { get; set; }
        public DateTime created_at { get; set; }
        public int created_by { get; set; }
        public List<int> members { get; set; }
    }
}
