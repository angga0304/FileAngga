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
    public class creditapiController : ApiController
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        // GET api/creditapi
        public IEnumerable<credit> Getcredits()
        {
            List<credit> liscredit = new List<credit>();
            foreach (var datacredit in db.credits)
            {
                credit realcredits = new credit();
                realcredits.creditId = datacredit.creditId;
                realcredits.creditDate = datacredit.creditDate;
                realcredits.MemberId = datacredit.MemberId;
                realcredits.ItemId = datacredit.ItemId;
                realcredits.creditke = datacredit.creditke;
                liscredit.Add(realcredits);
            }
            IEnumerable<credit> lis = liscredit;
            return lis;
        }

        // GET api/creditapi/5
        public credit Getcredit(int id)
        {
            credit credit = db.credits.Find(id);
            if (credit == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            credit real = new credit();
            real.creditId = credit.creditId;
            real.MemberId = credit.MemberId;
            real.ItemId = credit.ItemId;
            real.creditke = credit.creditke;
            real.creditDate = credit.creditDate;
            return real;
        }

        // PUT api/creditapi/5
        public HttpResponseMessage Putcredit(int id, credit credit)
        {
            if (ModelState.IsValid && id == credit.creditId)
            {
                db.Entry(credit).State = EntityState.Modified;

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

        // POST api/creditapi
        public HttpResponseMessage Postcredit(credit credit)
        {
            if (ModelState.IsValid)
            {
                db.credits.Add(credit);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, credit);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = credit.creditId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/creditapi/5
        public HttpResponseMessage Deletecredit(int id)
        {
            credit credit = db.credits.Find(id);
            if (credit == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.credits.Remove(credit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, credit);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}