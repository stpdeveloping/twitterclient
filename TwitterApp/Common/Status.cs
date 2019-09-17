using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterApp.Common
{
    public class Status
    {
        public string created_at { get; set; }
        public string text { get; set; }
        public User user { get; set; }
    }
}