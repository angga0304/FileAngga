using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MusicStoreClient.Models;

namespace MusicStoreClient.Controllers
{
    public class MemberController : Controller
    {
        //
        // GET: /Member/
    
        public async Task<ActionResult> Index(int id = 0)
        {
            List<Member> listmember = new List<Member>();
            if (id ==0)
                listmember = await getall();            
            else
                listmember = await getbyId(id);
            if (listmember !=null)
                return View(listmember);
            else
                return HttpNotFound();

        }

        public async Task<List<Member>> getall()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/memberapi/");
                var jsonstring = await response.Content.ReadAsStringAsync();
                var listmember = JsonConvert.DeserializeObject<List<Member>>(jsonstring);
                if (response.IsSuccessStatusCode)
                {
                    return listmember;
                }
                return null;
            } 
        }

        public async Task<List<Member>> getbyId(int id = 0)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/memberapi/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Member member = await response.Content.ReadAsAsync<Member>();
                    List<Member> listmember = new List<Member>();
                    listmember.Add(member);
                    return listmember;
                }
                return null;
            }
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(Member member)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:44093/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/memberapi/", member);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return HttpNotFound();
        }

        public async Task<ActionResult> Edit(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/memberapi/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Member member = await response.Content.ReadAsAsync<Member>();
                    return View(member);
                }
                return HttpNotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, Member member)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44093/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "http://localhost:44093/api/memberapi/" + id;
                HttpResponseMessage response = await client.PutAsJsonAsync(url, member);
                return RedirectToAction("Index");
            }
        }


    }
}
