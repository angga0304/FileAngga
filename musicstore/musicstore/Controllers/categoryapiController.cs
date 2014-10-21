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

namespace musicstore.Controllers
{
    public class categoryapiController : ApiController
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        // GET api/categoryapi
        public IEnumerable<category> Getcategories()
        {
            List<category> listcategory = new List<category>();
            foreach (var datacategory in db.categories)
            {
                category categories = new category();
                categories.CategoryId = datacategory.CategoryId;
                categories.CategoryName = datacategory.CategoryName;
                listcategory.Add(categories);
            }
            IEnumerable<category> listct = listcategory;
            return listct;
        }

        // GET api/categoryapi/5
        public category Getcategory(int id)
        {
            category category = db.categories.Find(id);
            if (category == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            category realct = new category();
            realct.CategoryId = category.CategoryId;
            realct.CategoryName = category.CategoryName;
            return realct;
        }

        // PUT api/categoryapi/5
        public HttpResponseMessage Putcategory(int id, category category)
        {
            if (ModelState.IsValid && id == category.CategoryId)
            {
                db.Entry(category).State = EntityState.Modified;

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

        // POST api/categoryapi
        public HttpResponseMessage Postcategory(category category)
        {
            if (ModelState.IsValid)
            {
                db.categories.Add(category);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, category);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = category.CategoryId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/categoryapi/5
        public HttpResponseMessage Deletecategory(int id)
        {
            category category = db.categories.Find(id);
            if (category == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.categories.Remove(category);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, category);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}