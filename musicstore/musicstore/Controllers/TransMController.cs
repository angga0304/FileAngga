using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using musicstore.Models;

namespace musicstore.Controllers
{
    public class TransMController : Controller
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        //
        // GET: /TransM/

        public ActionResult Index()
        {
            var trans_m = db.Trans_M.Include(t => t.Member);
            return View(trans_m.ToList());
        }

        public ViewResult indexjoin(int? page)
        {
            List<Trans_M> trans_m = new List<Trans_M>();
            foreach (var data in db.Trans_M)
            {
                Trans_M strans = new Trans_M();
                strans.TransId = data.TransId;
                strans.TransaDate = data.TransaDate;
                strans.MemberId = data.MemberId;
                strans.TotalTrans = data.TotalTrans;
                trans_m.Add(strans);
            }

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            var tr = from t in trans_m
                     join m in db.Members.ToList()
                     on t.MemberId equals m.MemberId
                     select new
                     {
                         t.TransId,
                         t.TransaDate,
                         m.MemberName,
                         t.TotalTrans,
                     };
            var listtrans = tr.ToPagedList(pageNumber, pageSize);
            return View(listtrans);
        }

        public ViewResult indexx(int? page)
        {
            List<Trans_M> trans_m = new List<Trans_M>();
            foreach (var data in db.Trans_M)
            {
                Trans_M strans = new Trans_M();
                strans.TransId = data.TransId;
                strans.TransaDate = data.TransaDate;
                strans.MemberId = data.MemberId;
                strans.TotalTrans = data.TotalTrans;
                trans_m.Add(strans);
            }  
            
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            var listtrans = trans_m.ToPagedList(pageNumber, pageSize);
            return View(listtrans);
        }

        private ViewResult View(IPagedList<Trans_M> listtrans, object listjoin)
        {
            throw new NotImplementedException();
        }

        //
        // GET: /TransM/Details/5

        public ActionResult Details(string id = null)
        {
            Trans_M trans_m = db.Trans_M.Find(id);
            if (trans_m == null)
            {
                return HttpNotFound();
            }
            return View(trans_m);
        }

        //
        // GET: /TransM/Create

        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName");
            return View();
        }

        //
        // POST: /TransM/Create

        [HttpPost]
        public ActionResult Create(Trans_M trans_m)
        {
            if (ModelState.IsValid)
            {
                db.Trans_M.Add(trans_m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", trans_m.MemberId);
            return View(trans_m);
        }

        //
        // GET: /TransM/Edit/5

        public ActionResult Edit(string id = null)
        {
            Trans_M trans_m = db.Trans_M.Find(id);
            if (trans_m == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", trans_m.MemberId);
            return View(trans_m);
        }

        //
        // POST: /TransM/Edit/5

        [HttpPost]
        public ActionResult Edit(Trans_M trans_m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trans_m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", trans_m.MemberId);
            return View(trans_m);
        }

        //
        // GET: /TransM/Delete/5

        public ActionResult Delete(string id = null)
        {
            Trans_M trans_m = db.Trans_M.Find(id);
            if (trans_m == null)
            {
                return HttpNotFound();
            }
            return View(trans_m);
        }

        //
        // POST: /TransM/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Trans_M trans_m = db.Trans_M.Find(id);
            db.Trans_M.Remove(trans_m);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}