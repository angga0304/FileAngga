using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStoreClient.Models
{
    public class credit
    {
        public int creditId { get; set; }
        public Nullable<int> MemberId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> creditke { get; set; }
        public Nullable<System.DateTime> creditDate { get; set; }
    }
}