using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Response
{
    public class GroupResponse
    {
        public int groupID { get; set; }
        public string group_name { get; set; }
        public int creatorID { get; set; }
        public string created_by { get; set; }
        public DateTime updated_at { get; set; }
        public List<MemberResponse> members { get; set; }
    }

}
