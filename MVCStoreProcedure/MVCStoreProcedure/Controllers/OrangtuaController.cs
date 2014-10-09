using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCStoreProcedure.Controllers
{
    public class OrangtuaController : Controller
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        //
        // GET: /Orangtua/

        public ActionResult Index()
        {
            return View(db.OrangTuas.ToList());
        }

        //
        // GET: /Orangtua/Details/5

        public ActionResult Details(string id = null)
        {
            OrangTua orangtua = db.OrangTuas.Find(id);
            if (orangtua == null)
            {
                return HttpNotFound();
            }
            return View(orangtua);
        }

        //
        // GET: /Orangtua/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Orangtua/Create

        [HttpPost]
        public ActionResult Create(OrangTua orangtua)
        {
            if (ModelState.IsValid)
            {
                db.OrangTuas.Add(orangtua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orangtua);
        }

        //
        // GET: /Orangtua/Edit/5

        public ActionResult Edit(string id = null)
        {
            OrangTua orangtua = db.OrangTuas.Find(id);
            if (orangtua == null)
            {
                return HttpNotFound();
            }
            return View(orangtua);
        }

        //
        // POST: /Orangtua/Edit/5

        [HttpPost]
        public ActionResult Edit(OrangTua orangtua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orangtua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orangtua);
        }

        //
        // GET: /Orangtua/Delete/5

        public ActionResult Delete(string id = null)
        {
            OrangTua orangtua = db.OrangTuas.Find(id);
            if (orangtua == null)
            {
                return HttpNotFound();
            }
            return View(orangtua);
        }

        //
        // POST: /Orangtua/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            OrangTua orangtua = db.OrangTuas.Find(id);
            db.OrangTuas.Remove(orangtua);
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