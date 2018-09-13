using Microsoft.AspNetCore.Mvc.ModelBinding;
using SecondSplitWise.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Response
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public user user { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}
