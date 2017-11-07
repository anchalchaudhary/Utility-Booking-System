using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityBookingSystem.Models;

namespace UtilityBookingSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login()
        {
            if(Convert.ToInt32(Session["loggedIn"])==1)
            {
                return RedirectToAction("Index");
            }
            Session["LoggedIn"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(BigViewModel model)
        {
            if (model.login.loginID == "admin" && model.login.password == "admin123")
            {
                Session["loggedIn"] = 1;
                return RedirectToAction("Index");
            }
            ViewBag.InvalidCredentials = "Invalid Credentials";
            return View();
        }
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["LoggedIn"]) == 1)
            {
                return View();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session["loggedIn"] = null;
            return RedirectToAction("Login");
        }
    }
}