using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using MusicStoreClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MusicStoreClient.Controllers
{
    public class ItemController : Controller
    {
        //
        // GET: /Item/

        public async Task<ActionResult> Index(int id = 0)
        {
            List<item> listitem = new List<item>();
            if (id == 0)
                listitem = await getall();
            else
            {
                item item = await getdetails(id);
                listitem.Add(item);
            }
            
            return View(listitem);
        }

        public async Task<List<item>> getall()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/itemapi");
                var jsonstring = await response.Content.ReadAsStringAsync();
                var listitem = JsonConvert.DeserializeObject<List<item>>(jsonstring);
                if (response.IsSuccessStatusCode)
                {
                    return listitem;
                }
                return null;
            }
        }

        public async Task<item> getdetails(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/itemapi/" + id);
                if (response.IsSuccessStatusCode)
                {
                    item item = await response.Content.ReadAsAsync<item>();
                    return item;
                }
                return null;
            }
        }

    }
}
