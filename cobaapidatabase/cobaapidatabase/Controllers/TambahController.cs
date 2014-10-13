using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cobaapidatabase.Controllers
{
    public class TambahController : Controller
    {
        private DbAnggaEntities db = new DbAnggaEntities();

        //
        // GET: /Tambah/

        public ActionResult Index()
        {
            return View(db.mahasiswas.ToList());
        }

        //
        // GET: /Tambah/Details/5

        public ActionResult Details(string id = null)
        {
            mahasiswa mahasiswa = db.mahasiswas.Find(id);
            if (mahasiswa == null)
            {
                return HttpNotFound();
            }
            return View(mahasiswa);
        }

        //
        // GET: /Tambah/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tambah/Create

        [HttpPost]
        public ActionResult Create(mahasiswa mahasiswa)
        {
            if (ModelState.IsValid)
            {
                db.mahasiswas.Add(mahasiswa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mahasiswa);
        }

        //
        // GET: /Tambah/Edit/5

        public ActionResult Edit(string id = null)
        {
            mahasiswa mahasiswa = db.mahasiswas.Find(id);
            if (mahasiswa == null)
            {
                return HttpNotFound();
            }
            return View(mahasiswa);
        }

        //
        // POST: /Tambah/Edit/5

        [HttpPost]
        public ActionResult Edit(mahasiswa mahasiswa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mahasiswa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mahasiswa);
        }

        //
        // GET: /Tambah/Delete/5

        public ActionResult Delete(string id = null)
        {
            mahasiswa mahasiswa = db.mahasiswas.Find(id);
            if (mahasiswa == null)
            {
                return HttpNotFound();
            }
            return View(mahasiswa);
        }

        //
        // POST: /Tambah/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            mahasiswa mahasiswa = db.mahasiswas.Find(id);
            db.mahasiswas.Remove(mahasiswa);
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