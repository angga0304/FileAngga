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
    public class CreditController : Controller
    {
        //
        // GET: /Credit/

        public async Task<ActionResult> Index(int id = 0)
        {
            List<credit> listcredit = new List<credit>();
            if (id == 0)
                listcredit = await getall();
            else
            {
                credit credit = await getcreditdata(id);
                listcredit.Add(credit);
            }
            return View(listcredit);
        }

        public async Task<List<credit>> getall()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/creditapi");
                var jsonstring = await response.Content.ReadAsStringAsync();
                var listcredit = JsonConvert.DeserializeObject<List<credit>>(jsonstring);
                if (response.IsSuccessStatusCode)
                {
                    return listcredit;
                }
                return null;
            }
        }

        public async Task<credit> getcreditdata(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/creditapi/" + id);
                if (response.IsSuccessStatusCode)
                {
                    credit credit = await response.Content.ReadAsAsync<credit>();
                    return credit;
                }
                return null;
            }
        }

        public ActionResult postcredit()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> postcredit(credit credit)
        {
            if(ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:44093/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/creditapi", credit);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("Index");
                }
            }
            return HttpNotFound();
        }

        public async Task<ActionResult> bayarcredit(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/creditapi/" + id);
                if (response.IsSuccessStatusCode)
                {
                    credit credit = await response.Content.ReadAsAsync<credit>();
                    credit.creditke = credit.creditke + 1;
                    DateTime tanggal = credit.creditDate.Value;
                    credit.creditDate = tanggal.AddMonths(1);
                    string url = "http://localhost:44093/api/creditapi/" + id;
                    HttpResponseMessage responseedit = await client.PutAsJsonAsync(url, credit);
                    return RedirectToAction("Index");
                }
                return null;
            }
        }

    }
}
