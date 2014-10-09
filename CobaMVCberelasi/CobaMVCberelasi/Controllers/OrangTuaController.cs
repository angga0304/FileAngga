using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CobaMVCberelasi.Controllers
{
    public class OrangTuaController : Controller
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        //
        // GET: /OrangTua/

        public ActionResult Index()
        {
            return View(db.OrangTuas.ToList());
        }

        //
        // GET: /OrangTua/Details/5

        public ActionResult Details(string id = null)
        {
            OrangTua orangtua = db.OrangTuas.Find(id);
            
            if (orangtua == null)
            {
                return HttpNotFound();
            }
            if (db.Anaks.Where(t => t.IdOT == id).Count() == 0)
            {
                ViewBag.dataanak = null;
            }
            else
            {
                ViewBag.dataanak = db.Anaks.Where(t => t.IdOT == id).ToList();
            }
            return View(orangtua);
        }

        //
        // GET: /OrangTua/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /OrangTua/Create

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
        // GET: /OrangTua/Edit/5

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
        // POST: /OrangTua/Edit/5

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
        // GET: /OrangTua/Delete/5

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
        // POST: /OrangTua/Delete/5

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