using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityBookingSystem.Models;
using UtilityBookingSystem.Repository;

namespace UtilityBookingSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        AdminPanelRepository objAdminPanelRepository = new AdminPanelRepository();
        Users objUsers = new Users();
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
                ViewBag.deptList= new SelectList(objAdminPanelRepository.getDepartmentsList(), "deptID", "department");
                return View();
            }
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Index(BigViewModel model)
        {
            bool detailsSaved;

            objUsers.name = model.users.name;
            objUsers.email = model.users.email;
            objUsers.deptID = model.users.deptID;
            objUsers.password = model.users.password;

            detailsSaved = objUsers.AddNewUser(objUsers); //Saves New User data to DB

            ViewBag.deptList = new SelectList(objAdminPanelRepository.getDepartmentsList(), "deptID", "department"); //Creates list of departments

            if (detailsSaved == true)
                TempData["AddedUser"] = "<script> alert('User Added');</script>";
            else
                TempData["AddedUser"] = "<script> alert('Try again');</script>";

            return View();
        }
        public ActionResult Logout()
        {
            Session["loggedIn"] = null;
            return RedirectToAction("Login");
        }
    }
}