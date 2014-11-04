using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResidentsDuesApp.Controllers
{
    public class DuesController : Controller
    {
        private ResidentsEntities db = new ResidentsEntities();

        //
        // GET: /Dues/

        public ActionResult Index(string id ="")
        {
            
            if (id != "")
            {
                int month, year;
                if (id.Length == 6)
                {
                     month = Convert.ToInt32(id.Substring(0, 2));
                     year = Convert.ToInt32(id.Substring(2, 4));
                }
                else
                {
                     month = Convert.ToInt32(id.Substring(0, 1));
                     year = Convert.ToInt32(id.Substring(1, 4));
                }
                ViewBag.listbulan = new SelectList(new[] {
                new SelectListItem{Text="January", Value="1"},
                new SelectListItem{Text="February", Value="2"},
                new SelectListItem{Text="March", Value="3"},
                new SelectListItem{Text="April", Value="4"},
                new SelectListItem{Text="May", Value="5"},
                new SelectListItem{Text="June", Value="6"},
                new SelectListItem{Text="July", Value="7"},
                new SelectListItem{Text="August", Value="8"},
                new SelectListItem{Text="September", Value="9"},
                new SelectListItem{Text="October", Value="10"},
                new SelectListItem{Text="November", Value="11"},
                new SelectListItem{Text="December", Value="12"}
            }, "Value", "Text", month);
                var yr = Enumerable.Range(1996, (DateTime.Now.Year - 1995)).Reverse().Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() });
                ViewBag.listyear = new SelectList(yr.ToList(), "Value", "Text", year);
                var dues = db.dues.Where(x => x.DuesDate.Value.Month == month
                        && x.DuesDate.Value.Year == year).Include(d => d.House);
                return View(dues);
            }
            else
            {
                ViewBag.listbulan = new SelectList(new[] {
                new SelectListItem{Text="January", Value="1"},
                new SelectListItem{Text="February", Value="2"},
                new SelectListItem{Text="March", Value="3"},
                new SelectListItem{Text="April", Value="4"},
                new SelectListItem{Text="May", Value="5"},
                new SelectListItem{Text="June", Value="6"},
                new SelectListItem{Text="July", Value="7"},
                new SelectListItem{Text="August", Value="8"},
                new SelectListItem{Text="September", Value="9"},
                new SelectListItem{Text="October", Value="10"},
                new SelectListItem{Text="November", Value="11"},
                new SelectListItem{Text="December", Value="12"}
            }, "Value", "Text", DateTime.Now.Month);
                var yr = Enumerable.Range(2010, (DateTime.Now.Year - 2009)).Reverse().Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() });
                ViewBag.listyear = new SelectList(yr.ToList(), "Value", "Text", DateTime.Now.Year);
                var dues = db.dues.Where(x => x.DuesDate.Value.Month == DateTime.Now.Month
                        && x.DuesDate.Value.Year == DateTime.Now.Year).Include(d => d.House);
                var listdue = dues.ToList();
                return View(dues);
            }
            
        }

        //
        // GET: /Dues/Details/5

        public ActionResult Details(int id = 0)
        {
            due due = db.dues.Find(id);
            if (due == null)
            {
                return HttpNotFound();
            }
            return View(due);
        }

        public List<House> ComBoxData()
        {
            List<House> combo = new List<House>();
            foreach(var data in db.Houses)
            {
                House house = new House();
                house.HouseID = data.HouseID;
                house.HouseNumber =string.Concat(data.Block,"-", data.HouseNumber);
                combo.Add(house);
            }
            return combo;
        }

        [HttpGet]
        public ActionResult PayDues()
        {
            //mencari minimal iuran
            var obj = db.NeighbourhoodWatches.FirstOrDefault();
            var harga = obj.Duesdef;

            //penomoran otomatis
            due due = db.dues.OrderByDescending(x => x.DuesID).FirstOrDefault();
            var no = due.DuesID;
            due = new due();
            due.DuesID = no + 1;
            due.DuesDate = DateTime.Now;
            due.Fee = harga;

            ViewBag.HouseID = new SelectList(ComBoxData(), "HouseID", "HouseNumber");
            return PartialView("_PayDues",due);
        }

        [HttpPost]
        public ActionResult PayDues(due due)
        {
            if (ModelState.IsValid)
            {
                db.dues.Add(due);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseID = new SelectList(ComBoxData(), "HouseID", "HouseNumber", due.HouseID);
            return PartialView("_PayDues", due);
        }

        //
        // GET: /Dues/Create

        public ActionResult Create()
        {
            //var sHouseID = new SelectList(db.Houses, "HouseID", "HouseNumber".Concat(" - ")).ToList();
            ViewBag.HouseID = new SelectList(db.Houses, "HouseID", "HouseNumber");
            return View();
        }

        //
        // POST: /Dues/Create

        [HttpPost]
        public ActionResult Create(due due)
        {
            if (ModelState.IsValid)
            {
                db.dues.Add(due);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseID = new SelectList(db.Houses, "HouseID", "HouseNumber", due.HouseID);
            return View(due);
        }

        //
        // GET: /Dues/Edit/5

        public ActionResult Edit(int id = 0)
        {
            due due = db.dues.Find(id);
            if (due == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseID = new SelectList(db.Houses, "HouseID", "HouseNumber", due.HouseID);
            return View(due);
        }

        //
        // POST: /Dues/Edit/5

        [HttpPost]
        public ActionResult Edit(due due)
        {
            if (ModelState.IsValid)
            {
                db.Entry(due).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseID = new SelectList(db.Houses, "HouseID", "HouseNumber", due.HouseID);
            return View(due);
        }

        //
        // GET: /Dues/Delete/5

        public ActionResult Delete(int id = 0)
        {
            due due = db.dues.Find(id);
            if (due == null)
            {
                return HttpNotFound();
            }
            return View(due);
        }

        //
        // POST: /Dues/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            due due = db.dues.Find(id);
            db.dues.Remove(due);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //public string UpdateDue(decimal due)
        //{
        //    var response = alter
        //}
    }
}