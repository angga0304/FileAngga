using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using MusicStoreClient.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MusicStoreClient.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberAdress { get; set; }
        public string Memberphone { get; set; }
    }
}