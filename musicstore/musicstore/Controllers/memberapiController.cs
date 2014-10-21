using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace musicstore.Controllers
{
    public class memberapiController : ApiController
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        public IEnumerable<Member> GetMembers()
        {
            List<Member> lismember = new List<Member>();
            
            foreach (var anggota in db.Members)
            {
                Member member = new Member();
                member.MemberId = anggota.MemberId;
                member.MemberName = anggota.MemberName;
                member.MemberAdress = anggota.MemberAdress;
                member.Memberphone = anggota.Memberphone;
                //member.Trans_M = anggota.Trans_M;
                //member.credits = anggota.credits;
                lismember.Add(member);
            }
            IEnumerable<Member> members = lismember;
            return members;
        }

        public Member getMember(int id)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            Member member2 = new Member();
            member2.MemberId = member.MemberId;
            member2.MemberName = member.MemberName;
            member2.MemberAdress = member.MemberAdress;
            member2.Memberphone = member.Memberphone;
            return member2;
        }

        public HttpResponseMessage Putmember(int id, Member member)
        {
            if (ModelState.IsValid && id == member.MemberId)
            {
                db.Entry(member).State = EntityState.Modified;

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

        public HttpResponseMessage Postmember(Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, member);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = member.MemberId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage Deletemember(int id)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Members.Remove(member);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, member);
        }
    }
}
