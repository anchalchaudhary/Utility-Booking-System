﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityBookingSystem.Models;
using UtilityBookingSystem.Repository;
using UtilityBookingSystem.Extra_Classes;

namespace UtilityBookingSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        
        #region Model objects
        Users objUsers = new Users();
        Departments objDepartments = new Departments();
        BookedDate objBookedDate = new BookedDate();
        Booking objBooking = new Booking();
        BookedRequirement objBookedRequirement = new BookedRequirement();
        BookedHall objBookedHall = new BookedHall();
        BookedSlot objBookedSlot = new BookedSlot();
        #endregion

        #region Admin Login
        public ActionResult Login()
        {
            if(Convert.ToInt32(Session["LoggedIn"])==1)
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
                Session["LoggedIn"] = 1;
                return RedirectToAction("Index");
            }
            ViewBag.InvalidCredentials = "Invalid Credentials";
            return View();
        }
        #endregion

        #region Admin Home
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["LoggedIn"]) == 1)
            {
                ViewBag.deptList= new SelectList(objDepartments.GetDepartmentsList(), "deptID", "department"); //Fetches Department List from Model Departments
                return View();
            }
            return RedirectToAction("Login");
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
                mailBody = "Hello "+model.users.name + ".You have been added.";
                Mail.Send_Mail(model.users.email, mailBody, mailSubject); //Sends Mail to the registered User.

                TempData["AddedUser"] = "<script> alert('User Added and Mail sent.');</script>";
            }
            else
                TempData["AddedUser"] = "<script> alert('Try again');</script>";

            return View();
        }
        #endregion

        #region View Registered Users
        public ActionResult ViewRegisteredUsers()
        {
            ViewBag.RegisteredUsersList = objUsers.GetRegisteredUsers(); //Fetches Registered Users list from model Users
            return View();
        }
        #endregion

        #region View Booking Requests
        public ActionResult ViewBookingRequests()
        {
            List<Booking> allBookingsList = objBooking.GetAllBookingsList();
            //List<BookedDate> allBookedDatesList = objBookedDate.GetAllBookedDatesList();

            ViewBag.allBookingsList = allBookingsList;

            return View(); 
        }
        #endregion
        [HttpPost]
        public ActionResult ViewApplication(int bookingID, int userID)
        {
            List<Users> userDetailsList = objUsers.GetUserDetails(userID);
            ViewBag.userDetails = userDetailsList;

            List<BookedDate> bookingDateList= objBookedDate.GetBookingDateList(bookingID);
            List<BookedHall> bookedHallList = objBookedHall.GetBookedHallsList(bookingDateList);
            List<BookedRequirement> bookedReqList = objBookedRequirement.GetBookedRequirementList(bookingID);
            List<BookedSlot> bookingSlotList = objBookedSlot.GetBookingSlotsList(bookedHallList);


            return View();
        }
        #endregion

        #region Admin Logout
        public ActionResult Logout()
        {
            Session["LoggedIn"] = null;
            return RedirectToAction("Login");
        }
        #endregion
    }
}