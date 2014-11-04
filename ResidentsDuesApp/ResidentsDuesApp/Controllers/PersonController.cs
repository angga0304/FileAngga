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

        public ActionResult Index(string id = "")
        {
            ViewBag.message = "";
            if (id == "")
            {
                var people = db.People.Include(p => p.House);
                return View(people.ToList());
            }
            else
            {
                var people = db.People.Where(a => a.Name.Contains(id)).Include(p => p.House);
                if (people.Count() == 0)
                {
                    ViewBag.message = "Tidak Ditemukan Orang yang Anda Cari";
                }
                return View(people.ToList());
            }
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

        //membuat Dropdown list dengan nilai houseID dan teks tampilan blok - number i.e:A-01
        public List<House> datacombobox()
        {
            List<House> combo = new List<House>();
            foreach (var data in db.Houses)
            {
                House house = new House();
                house.HouseID = data.HouseID;
                house.HouseNumber = string.Concat(data.Block, "-", data.HouseNumber);
                combo.Add(house);
            }
            return combo;
        }

        //
        // GET: /Person/Create

        public ActionResult Create()
        {
            //penomoran otomatis
            Person person = db.People.OrderByDescending(a => a.PeopleID).FirstOrDefault();
            var no = person.PeopleID;
            person = new Person();
            person.PeopleID = no + 1;

            ViewBag.HouseID = new SelectList(datacombobox(), "HouseID", "HouseNumber");

            return View(person);
        }

        public ActionResult CreatePartial()
        {
            Person person = db.People.OrderByDescending(a => a.PeopleID).FirstOrDefault();
            var no = person.PeopleID;
            person = new Person();
            person.PeopleID = no + 1;

            ViewBag.HouseID = new SelectList(datacombobox(), "HouseID", "HouseNumber");

            return PartialView("_FormPartial",person);
        }

        [HttpPost]
        public ActionResult CreatePartial(Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseID = new SelectList(datacombobox(), "HouseID", "HouseNumber", person.HouseID);
            return PartialView("_FormPartial", person);
        }

        public ActionResult EditPartial(int id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseID = new SelectList(datacombobox(), "HouseID", "HouseNumber", person.HouseID);
            return PartialView("_FormPartial", person);
        }

        [HttpPost]
        public ActionResult EditPartial(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseID = new SelectList(datacombobox(), "HouseID", "HouseNumber", person.HouseID);
            return PartialView("_FormPartial", person);
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

            ViewBag.HouseID = new SelectList(datacombobox(), "HouseID", "HouseNumber", person.HouseID);
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
            ViewBag.HouseID = new SelectList(datacombobox(), "HouseID", "HouseNumber", person.HouseID);
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
            ViewBag.HouseID = new SelectList(datacombobox(), "HouseID", "HouseNumber", person.HouseID);
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