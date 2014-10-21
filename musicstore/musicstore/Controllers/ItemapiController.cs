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
    public class ItemapiController : ApiController
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        // GET api/Itemapi
        public IEnumerable<item> Getitems()
        {
            List<item> lisitem = new List<item>();
            foreach (var itemdata in db.items)
            {
                item item = new item();
                item.ItemId = itemdata.ItemId;
                item.Category = itemdata.Category;
                item.ItemName = itemdata.ItemName;
                item.EventAct = itemdata.EventAct;
                item.even = itemdata.even;
                lisitem.Add(item);
            }
            IEnumerable<item> items= lisitem;
            return items;
        }

        // GET api/Itemapi/5
        public item Getitem(int id)
        {
            item item = db.items.Find(id);
            if (item == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            item dataitem = new item();
            dataitem.ItemId = item.ItemId;
            dataitem.ItemName = item.ItemName;
            dataitem.Category = item.Category;
            dataitem.EventAct = item.EventAct;
            dataitem.even = item.even;
            return dataitem;
        }

        // PUT api/Itemapi/5
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

        // POST api/Itemapi
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

        // DELETE api/Itemapi/5
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