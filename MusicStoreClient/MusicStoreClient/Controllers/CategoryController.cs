using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using MusicStoreClient.Models;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace MusicStoreClient.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public async Task<ActionResult> Index(int id = 0)
        {
            List<category> lisct = new List<category>();
            if (id == 0)
                lisct = await getall();
            else
            {
                category ct = await getbyId(id);
                lisct.Add(ct);
            }
            return View(lisct);
        }

        public async Task<List<category>> getall()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/categoryapi");
                var jsonstring = await response.Content.ReadAsStringAsync();
                var listcategory = JsonConvert.DeserializeObject<List<category>>(jsonstring);
                if (response.IsSuccessStatusCode)
                {
                    return listcategory;
                }
                return null;
            }
        }

        public async Task<category> getbyId(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/categoryapi/" + id);
                if (response.IsSuccessStatusCode)
                {
                    category category = await response.Content.ReadAsAsync<category>();
                    return category;
                }
                return null;
            }
        }

    }
}
