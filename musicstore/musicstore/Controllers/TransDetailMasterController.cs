using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using musicstore.Models;
using PagedList;

namespace musicstore.Controllers
{
    public class TransDetailMasterController : Controller
    {
        //
        // GET: /TransDetailMaster/
        public DbAnggaEntities db = new DbAnggaEntities();

        public ViewResult Index(int? page)
        {
            List<TransMasterDetailModel> listdm = new List<TransMasterDetailModel>();
            foreach (var data in db.Trans_M)
            {
                TransMasterDetailModel tdmdata = new TransMasterDetailModel();
                tdmdata.selectedTransM = data;
                tdmdata.TransMMember = db.Members.Find(tdmdata.selectedTransM.MemberId).MemberName;
                tdmdata.transdetails = db.Trans_d.Where(a => a.TransId == tdmdata.selectedTransM.TransId).ToList();
                listdm.Add(tdmdata);
            }
            int pagesize = 1;
            int pagenumber = (page ?? 1);
            ViewBag.barang = db.items.ToList();
            return View(listdm.ToPagedList(pagenumber,pagesize));
        }

        

    }
}
