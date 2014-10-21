using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace musicstore.Controllers
{
    public class CreditController : Controller
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        //
        // GET: /Credit/

        public ActionResult Index()
        {
            var credits = db.credits.Include(c => c.item).Include(c => c.Member);
            return View(credits.ToList());
        }

        public ActionResult IndexPartial(int id = 0)
        {
            var listcredit = db.credits.Where(a => a.MemberId == id).Include(c => c.item).Include(c => c.Member).ToList();
            if (listcredit.Count == 0)
            {
                ViewBag.nul = true;
                return PartialView("_IndexPartial", null);
            }
            ViewBag.nul = false;
            return PartialView("_IndexPartial", listcredit);
        }

        //
        // GET: /Credit/Details/5

        public ActionResult Details(int id = 0)
        {
            credit credit = db.credits.Find(id);
            if (credit == null)
            {
                return HttpNotFound();
            }
            return View(credit);
        }

        //
        // GET: /Credit/Create

        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.items, "ItemId", "ItemName");
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName");
            return View();
        }

        //
        // POST: /Credit/Create

        [HttpPost]
        public ActionResult Create(credit credit)
        {
            if (ModelState.IsValid)
            {
                db.credits.Add(credit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.items, "ItemId", "ItemName", credit.ItemId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", credit.MemberId);
            return View(credit);
        }

        //
        // GET: /Credit/Edit/5

        public ActionResult Edit(int id = 0)
        {
            credit credit = db.credits.Find(id);
            if (credit == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.items, "ItemId", "ItemName", credit.ItemId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", credit.MemberId);
            return View(credit);
        }

        //
        // POST: /Credit/Edit/5

        [HttpPost]
        public ActionResult Edit(credit credit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.items, "ItemId", "ItemName", credit.ItemId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", credit.MemberId);
            return View(credit);
        }

        //
        // GET: /Credit/Delete/5

        public ActionResult Delete(int id = 0)
        {
            credit credit = db.credits.Find(id);
            if (credit == null)
            {
                return HttpNotFound();
            }
            return View(credit);
        }

        //
        // POST: /Credit/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            credit credit = db.credits.Find(id);
            db.credits.Remove(credit);
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