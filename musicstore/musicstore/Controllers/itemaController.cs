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
    public class itemaController : ApiController
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        // GET api/itema
        public IEnumerable<item> Getitems()
        {
            var items = db.items.Include(i => i.category1);
            return items.AsEnumerable();
        }

        // GET api/itema/5
        public item Getitem(int id)
        {
            item item = db.items.Find(id);
            if (item == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return item;
        }

        // PUT api/itema/5
        public HttpResponseMessage Putitem(int id, item item)
        {
            if (ModelState.IsValid && id == item.ItemId)
            {
                db.Entry(item).State = EntityState.Modified;

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

        // POST api/itema
        public HttpResponseMessage Postitem(item item)
        {
            if (ModelState.IsValid)
            {
                db.items.Add(item);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, item);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = item.ItemId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/itema/5
        public HttpResponseMessage Deleteitem(int id)
        {
            item item = db.items.Find(id);
            if (item == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.items.Remove(item);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}