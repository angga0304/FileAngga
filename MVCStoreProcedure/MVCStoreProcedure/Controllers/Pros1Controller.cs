﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCStoreProcedure.Controllers
{
    public class Pros1Controller : Controller
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        //
        // GET: /Pros1/

        public ActionResult Index()
        {
            return View(db.Anaks.ToList());
        }

        //
        // GET: /Pros1/Details/5

        public ActionResult Details(string id = null)
        {
            Anak anak = db.Anaks.Find(id);
            if (anak == null)
            {
                return HttpNotFound();
            }
            return View(anak);
        }

        //
        // GET: /Pros1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Pros1/Create

        [HttpPost]
        public ActionResult Create(Anak anak)
        {
            if (ModelState.IsValid)
            {
                db.Anaks.Add(anak);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anak);
        }

        //
        // GET: /Pros1/Edit/5

        public ActionResult Edit(string id = null)
        {
            Anak anak = db.Anaks.Find(id);
            if (anak == null)
            {
                return HttpNotFound();
            }
            return View(anak);
        }

        //
        // POST: /Pros1/Edit/5

        [HttpPost]
        public ActionResult Edit(Anak anak)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anak).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anak);
        }

        //
        // GET: /Pros1/Delete/5

        public ActionResult Delete(string id = null)
        {
            Anak anak = db.Anaks.Find(id);
            if (anak == null)
            {
                return HttpNotFound();
            }
            return View(anak);
        }

        //
        // POST: /Pros1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Anak anak = db.Anaks.Find(id);
            db.Anaks.Remove(anak);
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