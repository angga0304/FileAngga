﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResidentsDuesApp.Controllers
{
    public class HouseController : Controller
    {
        private ResidentsEntities db = new ResidentsEntities();

        //
        // GET: /House/

        public ActionResult Index()
        {
            return View(db.Houses.ToList());
        }

        //
        // GET: /House/Details/5

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
        // GET: /House/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /House/Create

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
        // GET: /House/Edit/5

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
        // POST: /House/Edit/5

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

        //
        // GET: /House/Delete/5

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
        // POST: /House/Delete/5

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