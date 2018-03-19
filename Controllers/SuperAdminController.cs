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

        BookingSystemDBEntities db = new BookingSystemDBEntities();

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

                TempData["AddedUser"] = "<script src =\"https://unpkg.com/sweetalert/dist/sweetalert.min.js \"></script><script> swal('User added');</script>";
            }
            else
                TempData["AddedUser"] = "<script src =\"https://unpkg.com/sweetalert/dist/sweetalert.min.js \"></script><script> swal('This Department already has a user.');</script>";

            return View();
        }
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
        public ActionResult AddNewDepartment()
        {

            if (Convert.ToInt32(Session["AdminLoggedIn"]) == 1)
            {
                List<tblDepartment> listtblDepartment = objDepartments.GetDepartmentsList();
                ViewBag.departments = listtblDepartment;
                return View();
            }
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult AddNewDepartment(Departments model)
        {
            List<tblDepartment> listtblDepartment = objDepartments.GetDepartmentsList();
            ViewBag.departments = listtblDepartment;
            tblDepartment objtblDepartment = db.tblDepartments.FirstOrDefault(x => x.department.ToUpper() == model.department.ToUpper());
            if (objtblDepartment == null)
            {
                tblDepartment objtblDepartment1 = new tblDepartment();
                objtblDepartment1.department = model.department;
                db.tblDepartments.Add(objtblDepartment1);
                db.SaveChanges();

                TempData["AddedDept"] = "<script src =\"https://unpkg.com/sweetalert/dist/sweetalert.min.js \"></script><script> swal('Department added');</script>";

            }
            else
            {
                TempData["AddedDept"] = "<script src =\"https://unpkg.com/sweetalert/dist/sweetalert.min.js \"></script><script> swal('This Department is already added.')";
            }
            return RedirectToAction("AddNewDepartment");
        }
        public ActionResult DeleteDepartment(int deptID)
        {
            try
            {
                //int deptID = 1;
                tblDepartment objtblDepartment = new tblDepartment();
                using (BookingSystemDBEntities db = new BookingSystemDBEntities())
                {
                    objtblDepartment = db.tblDepartments.First(x => x.deptID == deptID);
                }
                using (BookingSystemDBEntities db = new BookingSystemDBEntities())
                {
                    db.tblDepartments.Attach(objtblDepartment);
                    db.tblDepartments.Remove(objtblDepartment);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                TempData["DeletedDept"] = "<script src =\"https://unpkg.com/sweetalert/dist/sweetalert.min.js \"></script><script> swal('Department Deleted')";
            }
            return RedirectToAction("AddNewDepartment", "SuperAdmin");
        }
        #endregion

        #region Admin Logout
        public ActionResult Logout()
        {
            Session["AdminLoggedIn"] = null;
            return RedirectToAction("Login", "SuperAdmin");
        }
        #endregion
    }
}