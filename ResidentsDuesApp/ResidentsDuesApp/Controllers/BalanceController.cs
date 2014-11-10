using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResidentsDuesApp.Models;

namespace ResidentsDuesApp.Controllers
{
    public class BalanceController : Controller
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
             SelectList listyear = new SelectList(yr.ToList(), "Value", "Text",tahun);
             return listyear;
        }

        //
        // GET: /Balance/
        ResidentsEntities db = new ResidentsEntities();

        public ActionResult Lihatsemua()
        {
            List<Balance> listbalance = new List<Balance>();
            foreach (var data in db.dues)
            {
                Balance balance = new Balance();
                balance.tgl = data.DuesDate.Value;
                balance.id = data.DuesID;
                var house = db.Houses.Where(a => a.HouseID == data.HouseID).FirstOrDefault();
                balance.keterangan = "Iuran Rumah " + house.Block + " - " + house.HouseNumber;
                balance.jumlah = data.Fee.Value;
                listbalance.Add(balance);
            }
            foreach (var data in db.Expenditures)
            {
                Balance balance = new Balance();
                balance.tgl = data.ExpenditureDate.Value;
                balance.id = data.ExpenditureID;
                balance.keterangan = data.Expenditure1;
                balance.jumlah = data.Fee.Value;
                listbalance.Add(balance);
            }
            return PartialView("LihatSemua",listbalance.OrderBy(a => a.tgl));
        }

        public ActionResult Index(string id ="")
        {
            List<Balance> listbalance = new List<Balance>();
            if (id == "")
            {
                foreach (var data in db.dues.Where(x => x.DuesDate.Value.Month == DateTime.Now.Month && x.DuesDate.Value.Year == DateTime.Now.Year))
                {
                    Balance balance = new Balance();
                    balance.tgl = data.DuesDate.Value;
                    balance.id = data.DuesID;
                    var house = db.Houses.Where(a => a.HouseID == data.HouseID).FirstOrDefault();
                    balance.keterangan = "Iuran Rumah " + house.Block + " - " + house.HouseNumber;
                    balance.jumlah = data.Fee.Value;
                    listbalance.Add(balance);
                }
                foreach (var data in db.Expenditures.Where(x => x.ExpenditureDate.Value.Month == DateTime.Now.Month && x.ExpenditureDate.Value.Year == DateTime.Now.Year))
                {
                    Balance balance = new Balance();
                    balance.tgl = data.ExpenditureDate.Value;
                    balance.id = data.ExpenditureID;
                    balance.keterangan = data.Expenditure1;
                    balance.jumlah = data.Fee.Value;
                    listbalance.Add(balance);
                }
                ViewBag.listbulan = getlistbulan(DateTime.Now.Month);
                ViewBag.listyear = getlisttahun(DateTime.Now.Year);
            }
            else
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
                foreach (var data in db.dues.Where(x => x.DuesDate.Value.Month == month && x.DuesDate.Value.Year == year))
                {
                    Balance balance = new Balance();
                    balance.tgl = data.DuesDate.Value;
                    balance.id = data.DuesID;
                    var house = db.Houses.Where(a => a.HouseID == data.HouseID).FirstOrDefault();
                    balance.keterangan = "Iuran Rumah " + house.Block + " - " + house.HouseNumber;
                    balance.jumlah = data.Fee.Value;
                    listbalance.Add(balance);
                }
                foreach (var data in db.Expenditures.Where(x => x.ExpenditureDate.Value.Month == month && x.ExpenditureDate.Value.Year == year))
                {
                    Balance balance = new Balance();
                    balance.tgl = data.ExpenditureDate.Value;
                    balance.id = data.ExpenditureID;
                    balance.keterangan = data.Expenditure1;
                    balance.jumlah = data.Fee.Value;
                    listbalance.Add(balance);
                }
                ViewBag.listbulan = getlistbulan(month);
                ViewBag.listyear = getlisttahun(year);
            }
            return View(listbalance.OrderBy(a => a.tgl));
        }

    }
}
//untuk melihat kas
//decimal kas =0;
//foreach (var data in listbalance.OrderBy(a=>a.tgl))
//{
//    if (data.keterangan.Contains("Iuran Rumah") || data.keterangan.Contains("Sisa Kas"))
//    {
//        kas += data.jumlah;
//    }
//    else
//        kas -= data.jumlah;
//}