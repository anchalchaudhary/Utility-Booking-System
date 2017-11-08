using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityBookingSystem.Models;
using UtilityBookingSystem.Repository;

namespace UtilityBookingSystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        Users objUsers = new Users();
        Purpose objPurpose = new Purpose();

        public ActionResult Login()
        {
            if (Convert.ToInt32(Session["UserLoggedIn"]) == 1)
            {
                return RedirectToAction("Index");
            }
            Session["UserLoggedIn"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(BigViewModel model)
        {
            bool isLoggedIn = objUsers.UserLogin(model);
            if(isLoggedIn==true)
            {
                Session["UserLoggedIn"] = 1;
                return RedirectToAction("Index");
            }
            ViewBag.InvalidCredentials = "Invalid Credentials";
            return View();
        }
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["UserLoggedIn"]) == 1)
            {
                ViewBag.purposeList = new SelectList(objPurpose.GetPurposeList(), "purposeID", "purpose");
                return View();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session["UserLoggedIn"] = null;
            return RedirectToAction("Login");
        }
    }
}