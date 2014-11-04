using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResidentsDuesApp.Controllers
{
    public class DuesController : Controller
    {
        private ResidentsEntities db = new ResidentsEntities();

        //
        // GET: /Dues/

        public ActionResult Index()
        {
            var dues = db.dues.Include(d => d.House);
            return View(dues.ToList());
        }

        //
        // GET: /Dues/Details/5

        public ActionResult Details(int id = 0)
        {
            due due = db.dues.Find(id);
            if (due == null)
            {
                return HttpNotFound();
            }
            return View(due);
        }

        //
        // GET: /Dues/Create

        public ActionResult Create()
        {
            //var sHouseID = new SelectList(db.Houses, "HouseID", "HouseNumber".Concat(" - ")).ToList();
            ViewBag.HouseID = new SelectList(db.Houses, "HouseID", "HouseNumber");
            return View();
        }



        //
        // POST: /Dues/Create

        [HttpPost]
        public ActionResult Create(due due)
        {
            if (ModelState.IsValid)
            {
                db.dues.Add(due);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseID = new SelectList(db.Houses, "HouseID", "HouseNumber", due.HouseID);
            return View(due);
        }

        //
        // GET: /Dues/Edit/5

        public ActionResult Edit(int id = 0)
        {
            due due = db.dues.Find(id);
            if (due == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseID = new SelectList(db.Houses, "HouseID", "HouseNumber", due.HouseID);
            return View(due);
        }

        //
        // POST: /Dues/Edit/5

        [HttpPost]
        public ActionResult Edit(due due)
        {
            if (ModelState.IsValid)
            {
                db.Entry(due).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseID = new SelectList(db.Houses, "HouseID", "HouseNumber", due.HouseID);
            return View(due);
        }

        //
        // GET: /Dues/Delete/5

        public ActionResult Delete(int id = 0)
        {
            due due = db.dues.Find(id);
            if (due == null)
            {
                return HttpNotFound();
            }
            return View(due);
        }

        //
        // POST: /Dues/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            due due = db.dues.Find(id);
            db.dues.Remove(due);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //public string UpdateDue(decimal due)
        //{
        //    var response = alter
        //}
    }
}