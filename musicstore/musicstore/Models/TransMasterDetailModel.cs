using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace musicstore.Models
{
    public class TransMasterDetailModel
    {
        public Trans_M selectedTransM { get; set; }
        public string TransMMember { get; set; }

        public List<Trans_d> transdetails { get; set; }
    }
}