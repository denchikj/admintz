using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingAdmin.Models
{
    public class menuMain
    {
        public Dictionary<int, string> menuProject { get; set; }
        public Dictionary<long, string> menuAccaunts { get; set; }
        public List<ItemMenuProfile> menuProfile { get; set; }
    }

    public class menuMainProfile
    {
        public long accountId { get; set; }
        public string accountName { get; set; }
    }
    public class ItemMenuProfile
    {
        public int id { get; set; }
        public string name { get; set; }
        public string guid { get; set; }
    }
}