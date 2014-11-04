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
        //
        // GET: /Balance/
        ResidentsEntities db = new ResidentsEntities();

        public ActionResult Index()
        {
            List<Balance> listbalance = new List<Balance>();
            foreach (var data in db.NeighbourhoodWatches)
            {
                Balance balance = new Balance();
                
                balance.id = data.NeighbourhoodID;
                balance.keterangan = "Uang Kas Bulan Lalu";
                balance.jumlah = data.Cash.Value;
                listbalance.Add(balance);
            }
            foreach (var data in db.dues)
            {
                Balance balance = new Balance();
                balance.tgl = data.DuesDate.Value;
                balance.id = data.DuesID;
                var house = db.Houses.Where(a=> a.HouseID == data.HouseID).FirstOrDefault();
                balance.keterangan = "Iuran Rumah "+ house.Block + house.HouseNumber;
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
            decimal kas =0;
            foreach (var data in listbalance.OrderBy(a=>a.tgl))
            {
                if (!data.keterangan.Contains("Iuran Rumah") || !data.keterangan.Contains("Uang Kas Bulan Lalu"))
                {
                    kas -= data.jumlah;
                }
                else
                    kas += data.jumlah;
            }
            return View(listbalance.OrderBy(a => a.tgl));
        }

    }
}
