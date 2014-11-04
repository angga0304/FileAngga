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
    public class TransMController : Controller
    {
        //
        // GET: /TransM/

        public async Task<ActionResult> Index(string id = "")
        {
            List<trans_m> listtrans = new List<trans_m>();
            if (id.Equals(""))
            {
                 listtrans = await getall();
            }
            else
            {
                trans_m transm = await getbyId(id);
                if(transm != null)
                    listtrans.Add(transm);
            }
            if (listtrans.Count == 0)
                return HttpNotFound();
            return View(listtrans);
        }

        public ActionResult posttransmst()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> posttransmst(trans_m trans_m)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:44093/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/trans_mapi/", trans_m);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return HttpNotFound();
        }

        public async Task<List<trans_m>> getall()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/trans_mapi/");
                var jsonstring = await response.Content.ReadAsStringAsync();
                var listtrans = JsonConvert.DeserializeObject<List<trans_m>>(jsonstring);
                if (response.IsSuccessStatusCode)
                {
                    return listtrans;
                }
                return null;
            } 
        }

        public async Task<trans_m> getbyId(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/trans_mapi/" + id);
                if (response.IsSuccessStatusCode)
                {
                    trans_m transm = await response.Content.ReadAsAsync<trans_m>();
                    return transm;
                }
                return null;
            }
        }



    }
}
