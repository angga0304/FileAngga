using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStoreClient.Models
{
    public class trans_mview
    {
        public string TransId { get; set; }
        public Nullable<System.DateTime> TransaDate { get; set; }
        public string MemberName { get; set; }
        public Nullable<decimal> TotalTrans { get; set; }
    }
}