using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityBookingSystem.Models;
using UtilityBookingSystem.Repository;
using UtilityBookingSystem.Extra_Classes;

namespace UtilityBookingSystem.Controllers
{
    public class SuperAdminController : Controller
    {
        #region Model objects
        Users objUsers = new Users();
        Departments objDepartments = new Departments();
        BookedDate objBookedDate = new BookedDate();
        Booking objBooking = new Booking();
        BookedRequirement objBookedRequirement = new BookedRequirement();
        BookedHall objBookedHall = new BookedHall();
        BookedSlot objBookedSlot = new BookedSlot();
        Chair objChair = new Chair();
        #endregion
        // GET: SuperAdmin
        #region Admin Login
        public ActionResult Login()
        {
            if (Convert.ToInt32(Session["AdminLoggedIn"]) == 1)
            {
                return RedirectToAction("Index");
            }
            Session["AdminLoggedIn"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(BigViewModel model)
        {
            if (model.login.loginID == "superadmin" && model.login.password == "superadmin123")
            {
                Session["AdminLoggedIn"] = 1;
                return RedirectToAction("Index");
            }
            ViewBag.InvalidCredentials = "Invalid Credentials";
            return View();
        }
        #endregion
        #region Admin Home
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["AdminLoggedIn"]) == 1)
            {
                ViewBag.deptList = new SelectList(objDepartments.GetDepartmentsList(), "deptID", "department"); //Fetches Department List from Model Departments
                return View();
            }
            return RedirectToAction("Login", "SuperAdmin");
        }
        #region Admin Adds User
        [HttpPost]
        public ActionResult Index(BigViewModel model)
        {
            bool detailsSaved = objUsers.AddNewUser(model); //Saves New User data to DB

            ViewBag.deptList = new SelectList(objDepartments.GetDepartmentsList(), "deptID", "department"); //Fetches Department List from Model Departments

            string mailSubject, mailBody;

            if (detailsSaved == true)
            {
                mailSubject = "Added";
                mailBody = "Hello " + model.users.name + ".You have been added.";
                Mail.Send_Mail(model.users.email, mailBody, mailSubject); //Sends Mail to the registered User.

                TempData["AddedUser"] = "<script> alert('User added');</script>";
            }
            else
                TempData["AddedUser"] = "<script> alert('Email already registered with us');</script>";

            return View();
        }
        #endregion
        #endregion
        #region View Registered Users
        public ActionResult ViewRegisteredUsers()
        {
            if (Convert.ToInt32(Session["AdminLoggedIn"]) == 1)
            {
                ViewBag.RegisteredUsersList = objUsers.GetRegisteredUsers(); //Fetches Registered Users list from model Users
                return View();
            }
            else
            {
                return RedirectToAction("Login", "SuperAdmin");
            }
        }
        #endregion
        public ActionResult DeleteUser(int userID)
        {
            bool isDeleted = objUsers.DeleteUser(userID);
            return RedirectToAction("ViewRegisteredUsers", "SuperAdmin");
        }
        #region Admin Logout
        public ActionResult Logout()
        {
            Session["AdminLoggedIn"] = null;
            return RedirectToAction("Login", "SuperAdmin");
        }
        #endregion
    }
}