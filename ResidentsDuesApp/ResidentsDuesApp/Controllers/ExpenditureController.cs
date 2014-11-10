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
        //membuat list bulan dan tahun
        public SelectList getlistbulan(int bulan)
        {
            SelectList listbulan = new SelectList(new[] {
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
            }, "Value", "Text", bulan);
            return listbulan;
        }
        public SelectList getlisttahun(int tahun)
        {
            var yr = Enumerable.Range(2010, (DateTime.Now.Year - 2009)).Reverse().Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() });
            SelectList listyear = new SelectList(yr.ToList(), "Value", "Text", tahun);
            return listyear;
        }

        private ResidentsEntities db = new ResidentsEntities();

        //
        // GET: /Expenditure/

        public ActionResult Index(string id="")
        {
            if (id == "")
            {
                ViewBag.listbulan = getlistbulan(DateTime.Now.Month);
                ViewBag.listyear = getlisttahun(DateTime.Now.Year);
                var listexp = db.Expenditures.Where(x => x.ExpenditureDate.Value.Month == DateTime.Now.Month && x.ExpenditureDate.Value.Year == DateTime.Now.Year);
                return View(listexp.ToList());
            }
            else
            {
                int bulan, tahun;
                if (id.Length == 6)
                {
                     bulan = Convert.ToInt32(id.Substring(0, 2));
                     tahun = Convert.ToInt32(id.Substring(2, 4));
                }
                else
                {
                     bulan = Convert.ToInt32(id.Substring(0, 1));
                     tahun = Convert.ToInt32(id.Substring(1, 4));
                }
                ViewBag.listbulan = getlistbulan(bulan);
                ViewBag.listyear = getlisttahun(tahun);
                var listexp = db.Expenditures.Where(x => x.ExpenditureDate.Value.Month == bulan && x.ExpenditureDate.Value.Year == tahun);
                return View(listexp.ToList());
            }
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

        public ActionResult EditPartial(int id = 0)
        {
            Expenditure expenditure = db.Expenditures.Find(id);
            if (expenditure == null)
            {
                return HttpNotFound();
            }
            return PartialView("CreatePartial", expenditure);
        }

        [HttpPost]
        public ActionResult EditPartial(Expenditure expenditure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expenditure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView("CreatePartial", expenditure);
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