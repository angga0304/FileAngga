using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResidentsDuesApp.Controllers
{
    public class PersonController : Controller
    {
        private ResidentsEntities db = new ResidentsEntities();

        //
        // GET: /Person/

        public ActionResult Index()
        {
            var people = db.People.Include(p => p.House);
            return View(people.ToList());
        }

        public ActionResult getpartial(int id)
        {
            return PartialView("_IndexPartial",db.People.Where(a => a.HouseID == id).ToList());
        }

        //
        // GET: /Person/Details/5

        public ActionResult Details(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // GET: /Person/Create

        public ActionResult Create()
        {
            ViewBag.HouseID = new SelectList(db.Houses, "HouseID", "HouseNumber");
            return View();
        }

        //
        // POST: /Person/Create

        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseID = new SelectList(db.Houses, "HouseID", "HouseNumber", person.HouseID);
            return View(person);
        }

        //
        // GET: /Person/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseID = new SelectList(db.Houses, "HouseID", "HouseNumber", person.HouseID);
            return View(person);
        }

        //
        // POST: /Person/Edit/5

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseID = new SelectList(db.Houses, "HouseID", "HouseNumber", person.HouseID);
            return View(person);
        }

        //
        // GET: /Person/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // POST: /Person/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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