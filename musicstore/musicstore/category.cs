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
    
    public partial class category
    {
        public category()
        {
            this.items = new HashSet<item>();
        }
    
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    
        public virtual ICollection<item> items { get; set; }
    }
}
