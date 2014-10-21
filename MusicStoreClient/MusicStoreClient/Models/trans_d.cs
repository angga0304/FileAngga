using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStoreClient.Models
{
    public class trans_d
    {
        public string TransId { get; set; }
        public int ItemId { get; set; }
        public Nullable<int> qnt { get; set; }
        public string pricemethod { get; set; }
    }
}