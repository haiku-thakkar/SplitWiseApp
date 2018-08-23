using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class messageResponse
    {
        public bool Status { get; set; }
        public Messages messages { get; set; }
        public ModelStateDictionary ModelState { get; internal set; }

    }
}
