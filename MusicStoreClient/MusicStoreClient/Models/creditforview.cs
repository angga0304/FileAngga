using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStoreClient.Models
{
    public class creditforview
    {
        public int creditId { get; set; }
        public String MemberName { get; set; }
        public String ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public Nullable<int> creditke { get; set; }
        public Nullable<System.DateTime> creditDate { get; set; }
    }
}