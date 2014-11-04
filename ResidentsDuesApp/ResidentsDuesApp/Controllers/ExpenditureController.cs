using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResidentsDuesApp.Controllers
{
    public class ExpenditureController : Controller
    {
        private ResidentsEntities db = new ResidentsEntities();

        //
        // GET: /Expenditure/

        public ActionResult Index()
        {
            return View(db.Expenditures.ToList());
        }

        //
        // GET: /Expenditure/Details/5

        public ActionResult Details(int id = 0)
        {
            Expenditure expenditure = db.Expenditures.Find(id);
            if (expenditure == null)
            {
                return HttpNotFound();
            }
            return View(expenditure);
        }

        //
        // GET: /Expenditure/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Expenditure/Create

        [HttpPost]
        public ActionResult Create(Expenditure expenditure)
        {
            if (ModelState.IsValid)
            {
                db.Expenditures.Add(expenditure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expenditure);
        }

        public ActionResult CreatePartial()
        {
            Expenditure exp = db.Expenditures.OrderByDescending(x => x.ExpenditureID).FirstOrDefault();
            var no = exp.ExpenditureID;
            exp = new Expenditure();
            exp.ExpenditureID = no + 1;
            return PartialView("CreatePartial",exp);
        }

        [HttpPost]
        public ActionResult CreatePartial(Expenditure expenditure)
        {
            if (ModelState.IsValid)
            {
                db.Expenditures.Add(expenditure);
                db.SaveChanges();
                expenditure = null;
                return RedirectToAction("/Home/Index/");
            }

            return View("CreatePartial",expenditure);
        }

        //
        // GET: /Expenditure/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Expenditure expenditure = db.Expenditures.Find(id);
            if (expenditure == null)
            {
                return HttpNotFound();
            }
            return View(expenditure);
        }

        //
        // POST: /Expenditure/Edit/5

        [HttpPost]
        public ActionResult Edit(Expenditure expenditure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expenditure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenditure);
        }

        //
        // GET: /Expenditure/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Expenditure expenditure = db.Expenditures.Find(id);
            if (expenditure == null)
            {
                return HttpNotFound();
            }
            return View(expenditure);
        }

        //
        // POST: /Expenditure/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Expenditure expenditure = db.Expenditures.Find(id);
            db.Expenditures.Remove(expenditure);
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