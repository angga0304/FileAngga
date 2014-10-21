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
    public class transdapiController : ApiController
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        // GET api/transdapi
        public IEnumerable<Trans_d> GetTrans_d()
        {
            List<Trans_d> listtrans = new List<Trans_d>();
            foreach (var data in db.Trans_d)
            {
                Trans_d trans = new Trans_d();
                trans.TransId = data.TransId;
                trans.ItemId = data.ItemId;
                trans.qnt = data.qnt;
                trans.pricemethod = data.pricemethod;
                listtrans.Add(trans);
            }
            IEnumerable<Trans_d> lis = listtrans;
            return lis;
        }

        // GET api/transdapi/5
        public Trans_d GetTrans_d(string id)
        {
            Trans_d trans_d = db.Trans_d.Find(id);
            if (trans_d == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return trans_d;
        }

        // PUT api/transdapi/5
        public HttpResponseMessage PutTrans_d(string id, Trans_d trans_d)
        {
            if (ModelState.IsValid && id == trans_d.TransId)
            {
                db.Entry(trans_d).State = EntityState.Modified;

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

        // POST api/transdapi
        public HttpResponseMessage PostTrans_d(Trans_d trans_d)
        {
            if (ModelState.IsValid)
            {
                db.Trans_d.Add(trans_d);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, trans_d);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = trans_d.TransId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/transdapi/5
        public HttpResponseMessage DeleteTrans_d(string id)
        {
            Trans_d trans_d = db.Trans_d.Find(id);
            if (trans_d == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Trans_d.Remove(trans_d);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, trans_d);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}