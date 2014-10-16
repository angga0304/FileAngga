using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using cobaclientlagi.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace cobaclientlagi.Controllers
{
    public class KlienController : Controller
    {
        //
        // GET: /Klien/

        public async Task<ActionResult> Index(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:27160/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await client.GetAsync("api/Siswa/"+ id);
                if (response.IsSuccessStatusCode)
                {
                    Mahasiswa siswa = await response.Content.ReadAsAsync<Mahasiswa>();
                    string nama = siswa.NamaPertama;
                    return View(siswa);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        public async Task<ActionResult> getall()
        {
            //return View(listsiswa);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:27160/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Siswa");
                var jsonstring = await response.Content.ReadAsStringAsync();
                var listsiswa = JsonConvert.DeserializeObject<List<Mahasiswa>>(jsonstring);
                if (response.IsSuccessStatusCode)
                {
                    //return RedirectToAction("getall", listsiswa.ToList());
                    return View(listsiswa);
                }
            }
            return HttpNotFound();
        }

        public ActionResult getallsiswa(List<Mahasiswa> siswa)
        {
            return View(siswa);
        }

        public async Task<ActionResult> AllSiswa()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:27160/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Siswa");
                var jsonstring = await response.Content.ReadAsStringAsync();
                var listsiswa = JsonConvert.DeserializeObject<List<Mahasiswa>>(jsonstring);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("getallsiswa",listsiswa.ToList());
                }
            }
            return HttpNotFound();
        }



        public ActionResult postsiswa()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> postsiswa(Mahasiswa siswa)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:27160/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // New code:
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/Siswa/", siswa);
                    if (response.IsSuccessStatusCode)
                    {
                        Uri siswaurl = response.Headers.Location;
                        return RedirectToAction("Index");
                    }
                }
            }
            return HttpNotFound();
        }

        public async Task<ActionResult> delete(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:27160/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await client.GetAsync("api/Siswa/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Mahasiswa siswa = await response.Content.ReadAsAsync<Mahasiswa>();
                    string nama = siswa.NamaPertama;
                    return View(siswa);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }


        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> deleteconfirm(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:27160/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync("http://localhost:27160/api/Siswa/" + id);
                return RedirectToAction("getall");
            }
        }

        public async Task<ActionResult> edit(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:27160/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await client.GetAsync("api/Siswa/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Mahasiswa siswa = await response.Content.ReadAsAsync<Mahasiswa>();
                    string nama = siswa.NamaPertama;
                    return View(siswa);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> edit(string id,string NamaPertama, Mahasiswa siswa)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:27160/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string url = "http://localhost:27160/api/Siswa/" + id;
                HttpResponseMessage response = await client.PutAsJsonAsync(url,siswa);
                return RedirectToAction("getall");
            }
        }

    }
}
