using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStoreClient.Models
{
    public class item
    {
        public int ItemId { get; set; }
        public Nullable<int> Category { get; set; }
        public string ItemName { get; set; }
        public string EventAct { get; set; }
        public string even { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}