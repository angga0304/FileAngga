using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cobaapidatabase.Models;

namespace cobaapidatabase.Controllers
{
    public class MahasiswaController : ApiController
    {
        static readonly iMahasiswa repository = new mahasiswaRepository();

        public IEnumerable<mahasiswa> GetAll()
        {
            return repository.GetAll();
        }

        public mahasiswa GetProduct(string id)
        {
            mahasiswa item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public IEnumerable<mahasiswa> GetSiswaByName(String name)
        {
            return repository.GetAll().Where(s => string.Equals(s.NamaPertama, name, StringComparison.OrdinalIgnoreCase));
        }

        public HttpResponseMessage PostSiswa(mahasiswa item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<mahasiswa>(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { id = item.MhsId });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void DeleteSiswa(string id)
        {
            mahasiswa item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.Remove(id);
        }

        public void PutSiswa(string id,mahasiswa siswa)
        {
            siswa.MhsId = id;
            if (!repository.Update(siswa))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }

    }
}
