using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webapimvc2.Controllers
{
    public class ActionController : Controller
    {
        //
        // GET: /Action/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult add()
        {
            return View();
        }

        public ActionResult edit(string id)
        {
            ViewBag.id = id;
            return View();
        }

    }
}
