using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResidentsDuesApp.Models
{
    public class Balance
    {
        public DateTime tgl { get; set; }
        public int id { get; set; }
        public string keterangan { get; set; }
        public decimal jumlah { get; set; }
        public decimal total { get; set; }
    }
}