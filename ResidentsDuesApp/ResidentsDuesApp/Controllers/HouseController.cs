using System;
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

        public ActionResult Index(string id="")
        {
            ViewBag.message = "";
            List<House> listhouse = new List<House>();
            if (id.Equals(""))
                listhouse = db.Houses.ToList();
            else
            {
                int count = id.Length;
                if (count == 4)
                    listhouse = db.Houses.Where(a => a.Block.Equals(id.Substring(0, 1)) && a.HouseNumber.Equals(id.Substring(2,2))).ToList();
                else
                    listhouse = db.Houses.Where(a=> a.Block.Contains(id) || a.HouseNumber.Contains(id)).ToList();
                if(listhouse.Count == 0)
                    ViewBag.message = "Data Rumah yang Dicari Tidak ditemukan";
            }
            return View(listhouse);
        }

        public House getHouseData(int id = 0)
        {
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return null;
            }
            house.People = null;
            house.dues = null;
            return house;
        }


        public ActionResult createpartial()
        {
            House house = db.Houses.OrderByDescending(a => a.HouseID).FirstOrDefault();
            int no = house.HouseID;
            house = new House();
            house.HouseID = no + 1;
            return PartialView("_editpartial", house);
        }

        [HttpPost]
        public ActionResult createpartial(House house)
        {
            if (ModelState.IsValid)
            {
                db.Houses.Add(house);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(house);
        }

        public ActionResult editpartial(int id = 0)
        {
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return null;
            }
            house.People = null;
            house.dues = null;
            return PartialView("_editpartial",house);
        }

        [HttpPost]
        public ActionResult editpartial(House house)
        {
            if (ModelState.IsValid)
            {
                db.Entry(house).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView("_editpartial",house);
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
            House house = db.Houses.OrderByDescending(a => a.HouseID).FirstOrDefault();
            int no = house.HouseID;
            house = new House();
            house.HouseID = no+1;
            return View(house);
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