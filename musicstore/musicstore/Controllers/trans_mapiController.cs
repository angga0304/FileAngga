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
    public class trans_mapiController : ApiController
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        // GET api/trans_mapi
        public IEnumerable<Trans_M> GetTrans_M()
        {
            List<Trans_M> listrans= new List<Trans_M>();
            foreach (var data in db.Trans_M)
            {
                Trans_M trans = new Trans_M();
                trans.TransId = data.TransId;
                trans.TransaDate = data.TransaDate;
                trans.MemberId = data.MemberId;
                trans.TotalTrans = data.TotalTrans;
                listrans.Add(trans);
            }
            IEnumerable<Trans_M> lis = listrans;
            return lis;
        }

        // GET api/trans_mapi/5
        public Trans_M GetTrans_M(string id)
        {
            Trans_M trans_m = db.Trans_M.Find(id);
            if (trans_m == null)
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return null;
            }
            Trans_M data = new Trans_M();
            data.TransId = trans_m.TransId;
            data.TransaDate = trans_m.TransaDate;
            data.MemberId = trans_m.MemberId;
            data.TotalTrans = trans_m.TotalTrans;
            return data;
        }

        // PUT api/trans_mapi/5
        public HttpResponseMessage PutTrans_M(string id, Trans_M trans_m)
        {
            if (ModelState.IsValid && id == trans_m.TransId)
            {
                db.Entry(trans_m).State = EntityState.Modified;

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

        // POST api/trans_mapi
        public HttpResponseMessage PostTrans_M(Trans_M trans_m)
        {
            if (ModelState.IsValid)
            {
                db.Trans_M.Add(trans_m);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, trans_m);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = trans_m.TransId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/trans_mapi/5
        public HttpResponseMessage DeleteTrans_M(string id)
        {
            Trans_M trans_m = db.Trans_M.Find(id);
            if (trans_m == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Trans_M.Remove(trans_m);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, trans_m);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}