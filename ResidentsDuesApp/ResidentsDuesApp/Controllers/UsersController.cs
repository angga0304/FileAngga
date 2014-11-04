using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResidentsDuesApp.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        ResidentsEntities db = new ResidentsEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string id, string password)
        {
            user user =  db.users.Where(s => s.UserID.Equals(id) && s.Userpassword.Equals(password)).FirstOrDefault();
            if (user !=null)
            {
                Session["UserName"] = user.UserName;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["UserName"] = "";
            return RedirectToAction("Index", "Home");
        }

    }
}
