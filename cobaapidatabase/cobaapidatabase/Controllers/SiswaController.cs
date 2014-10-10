using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace cobaapidatabase.Controllers
{
    public class SiswaController : ApiController
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        // GET api/Siswa
        public IEnumerable<mahasiswa> Getmahasiswas()
        {
            return db.mahasiswas.AsEnumerable();
        }

        public IEnumerable<mahasiswa> Getfromyear(string tahun)
        {
            return db.mahasiswas.Where(a => a.MhsId.Substring(0,4).Equals(tahun)).AsEnumerable();
        }

        // GET api/Siswa/5
        public mahasiswa Getmahasiswa(string id)
        {
            mahasiswa mahasiswa = db.mahasiswas.Find(id);
            if (mahasiswa == null)
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return null;
            }

            return mahasiswa;
        }

        // PUT api/Siswa/5
        public HttpResponseMessage Putmahasiswa(string id, mahasiswa mahasiswa)
        {
            if (ModelState.IsValid && id == mahasiswa.MhsId)
            {
                db.Entry(mahasiswa).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Siswa
        public HttpResponseMessage Postmahasiswa(mahasiswa mahasiswa)
        {
            if (ModelState.IsValid)
            {
                db.mahasiswas.Add(mahasiswa);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, mahasiswa);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = mahasiswa.MhsId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Siswa/5
        public HttpResponseMessage Deletemahasiswa(string id)
        {
            mahasiswa mahasiswa = db.mahasiswas.Find(id);
            if (mahasiswa == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.mahasiswas.Remove(mahasiswa);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, mahasiswa);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}