using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStoreClient.Models
{
    public class itemjoincategory
    {
        public int ItemId { get; set; }
        public string ItemName {get;set;} 
        public decimal Price {get;set;}
        public string CategoryName {get;set;}
        public string even{get;set;}
        public string EventAct { get; set; }

    }
}