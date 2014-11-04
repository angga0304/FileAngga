using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResidentsDuesApp.Controllers
{
    public class House2Controller : Controller
    {
        private ResidentsEntities db = new ResidentsEntities();

        //
        // GET: /House2/

        public ActionResult Index()
        {
            return View(db.Houses.ToList());
        }

        //
        // GET: /House2/Details/5

        public ActionResult Details(int id = 0)
        {
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }

        //
        // GET: /House2/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /House2/Create

        [HttpPost]
        public ActionResult Create(House house)
        {
            if (ModelState.IsValid)
            {
                db.Houses.Add(house);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(house);
        }

        //
        // GET: /House2/Edit/5

        public ActionResult Edit(int id = 0)
        {
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }

        //
        // POST: /House2/Edit/5

        [HttpPost]
        public ActionResult Edit(House house)
        {
            if (ModelState.IsValid)
            {
                db.Entry(house).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(house);
        }

        public ActionResult editontable(int id=0)
        {
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return PartialView("_editontable", house);
        }

        [HttpPost]
        public ActionResult editontable(House house)
        {
            if (ModelState.IsValid)
            {
                db.Entry(house).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView("_editontable", house);
        }

        //
        // GET: /House2/Delete/5

        public ActionResult Delete(int id = 0)
        {
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }

        //
        // POST: /House2/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            House house = db.Houses.Find(id);
            db.Houses.Remove(house);
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