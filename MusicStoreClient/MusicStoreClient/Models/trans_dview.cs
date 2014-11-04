using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStoreClient.Models
{
    public class trans_dview
    {
        public string TransId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public Nullable<int> qnt { get; set; }
        public string pricemethod { get; set; }
    }
}