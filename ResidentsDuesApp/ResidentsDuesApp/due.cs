//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ResidentsDuesApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class due
    {
        public int DuesID { get; set; }
        public Nullable<System.DateTime> DuesDate { get; set; }
        public Nullable<int> HouseID { get; set; }
        public Nullable<decimal> Fee { get; set; }
    
        public virtual House House { get; set; }
    }
}
