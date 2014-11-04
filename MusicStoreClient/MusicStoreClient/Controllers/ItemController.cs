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
        CategoryController mc = new CategoryController();

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
            var category = await mc.getall();
            var listjoin = (from i in listitem.ToList()
                            join c in category.ToList()
                            on i.Category equals c.CategoryId
                            select new
                            {
                                i.ItemId,
                                i.ItemName,
                                i.Price,
                                c.CategoryName,
                                i.even,
                                i.EventAct
                            }).ToList();
            List<itemjoincategory> listijc = new List<itemjoincategory>();
            foreach(var data in listjoin)
            {
                itemjoincategory ijc = new itemjoincategory();
                ijc.ItemId = data.ItemId;
                ijc.ItemName = data.ItemName;
                ijc.Price = data.Price.Value;
                ijc.CategoryName = data.CategoryName;
                ijc.even = data.even;
                ijc.EventAct = data.EventAct;
                listijc.Add(ijc);
            }
            
            return View(listijc.ToList());
        }

        public async Task<ActionResult> testforhome()
        {
            List<item> listitem = await getsampleforhome();
            ViewBag.listtools = await gettoolsforhome();
            return View(listitem);
        }

        public ActionResult viewforhome(List<item> lis)
        {
            return PartialView("_homeview", lis);
        }

        public async Task<ActionResult> tes()
        {
            List<item> listitem = await getsampleforhome();
            return RedirectToActionPermanent("viewforhome", listitem);
        }

        public async Task<List<item>> getsampleforhome()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/itemapi");
                var jsonstring = await response.Content.ReadAsStringAsync();
                var listitem = JsonConvert.DeserializeObject<IEnumerable<item>>(jsonstring);
                if (response.IsSuccessStatusCode)
                {

                    return listitem.Where(a=>a.Category == 1).OrderByDescending(a => a.ItemId).Take(4).ToList();
                }
                return null;
            }
        }

        public async Task<List<item>> gettoolsforhome()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/itemapi");
                var jsonstring = await response.Content.ReadAsStringAsync();
                var listitem = JsonConvert.DeserializeObject<IEnumerable<item>>(jsonstring);
                if (response.IsSuccessStatusCode)
                {

                    return listitem.Where(a => a.Category == 2).OrderByDescending(a => a.ItemId).Take(4).ToList();
                }
                return null;
            }
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

        public async Task<ActionResult> makehomepage()
        {
            return View();
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
