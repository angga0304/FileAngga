using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cobaclientlagi.Models
{
    public class Mahasiswa
    {
        public string MhsId { get; set; }
        public string NamaPertama { get; set; }
        public string NamaTerakhir { get; set; }
        public Nullable<int> umur { get; set; }
        public string jnsk { get; set; }
        public string angkatan { get; set; }
        public string ipk { get; set; }
    }
}