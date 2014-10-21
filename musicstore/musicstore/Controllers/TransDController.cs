using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace musicstore.Controllers
{
    public class TransDController : Controller
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        //
        // GET: /TransD/

        public ActionResult Index()
        {
            var trans_d = db.Trans_d.Include(t => t.item).Include(t => t.Trans_M);
            return View(trans_d.ToList());
        }

        public ActionResult indexx(string id)
        {
            var trans_d = db.Trans_d.Where(a => a.TransId == id).Include(t => t.item).Include(t => t.Trans_M);
            return PartialView("_indexx", trans_d.ToList());
        }

        //public ActionResult Indexx()
        //{
        
        //}

        //
        // GET: /TransD/Details/5

        public ActionResult Details(string id = null)
        {
            Trans_d trans_d = db.Trans_d.Find(id);
            if (trans_d == null)
            {
                return HttpNotFound();
            }
            return View(trans_d);
        }

        //
        // GET: /TransD/Create

        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.items, "ItemId", "ItemName");
            ViewBag.TransId = new SelectList(db.Trans_M, "TransId", "TransId");
            return View();
        }

        //
        // POST: /TransD/Create

        [HttpPost]
        public ActionResult Create(Trans_d trans_d)
        {
            if (ModelState.IsValid)
            {
                db.Trans_d.Add(trans_d);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.items, "ItemId", "ItemName", trans_d.ItemId);
            ViewBag.TransId = new SelectList(db.Trans_M, "TransId", "TransId", trans_d.TransId);
            return View(trans_d);
        }

        //
        // GET: /TransD/Edit/5

        public ActionResult Edit(string id = null,int brg = 0)
        {
            Trans_d trans_d = db.Trans_d.Where(a => a.TransId.Equals(id) && a.ItemId == brg).FirstOrDefault();
            if (trans_d == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.items, "ItemId", "ItemName", trans_d.ItemId);
            ViewBag.TransId = new SelectList(db.Trans_M, "TransId", "TransId", trans_d.TransId);
            return View(trans_d);
        }

        //
        // POST: /TransD/Edit/5

        [HttpPost]
        public ActionResult Edit(Trans_d trans_d)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trans_d).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("indexx","TransM");
            }
            ViewBag.ItemId = new SelectList(db.items, "ItemId", "ItemName", trans_d.ItemId);
            ViewBag.TransId = new SelectList(db.Trans_M, "TransId", "TransId", trans_d.TransId);
            return View(trans_d);
        }

        //
        // GET: /TransD/Delete/5

        public ActionResult Delete(string id = null,int brg = 0)
        {
            Trans_d trans_d = db.Trans_d.Where(a => a.TransId.Equals(id) && a.ItemId == brg).FirstOrDefault();
            if (trans_d == null)
            {
                return HttpNotFound();
            }
            return View(trans_d);
        }

        //
        // POST: /TransD/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id,int brg)
        {
            Trans_d trans_d = db.Trans_d.Where(a => a.TransId.Equals(id) && a.ItemId == brg).FirstOrDefault();
            db.Trans_d.Remove(trans_d);
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