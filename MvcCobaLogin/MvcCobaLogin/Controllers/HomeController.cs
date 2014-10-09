using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCobaLogin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult login()
        {
            if (Session["LogeduserId"] != null)
            {
                return RedirectToAction("Index", "Anak");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult login(user u)
        {
            if (ModelState.IsValid)
            {
                using (DbAnggaEntities db = new DbAnggaEntities())
                {
                    var v = db.users.Where(a => a.username.Equals(u.username) && a.userpassword.Equals(u.userpassword)).FirstOrDefault();
                    if (v != null)
                    {
                        Session.Clear();
                        Session["LogeduserId"] = v.userid.ToString();
                        Session["LogedUserName"] = v.username.ToString();
                        return RedirectToAction("Index", "Anak");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User atau password salah");
                        return View(u);
                    }
                }
            }
            else
            { return View(u); }
        }

        public ActionResult logout()
        {
            Session["LogeduserId"] = null;
            Session["LogedUserName"] = null;
            return RedirectToAction("Login", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
