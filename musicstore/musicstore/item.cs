//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace musicstore
{
    using System;
    using System.Collections.Generic;
    
    public partial class item
    {
        public item()
        {
            this.credits = new HashSet<credit>();
            this.Trans_d = new HashSet<Trans_d>();
        }
    
        public int ItemId { get; set; }
        public Nullable<int> Category { get; set; }
        public string ItemName { get; set; }
        public string EventAct { get; set; }
        public string even { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual category category1 { get; set; }
        public virtual ICollection<credit> credits { get; set; }
        public virtual ICollection<Trans_d> Trans_d { get; set; }
    }
}