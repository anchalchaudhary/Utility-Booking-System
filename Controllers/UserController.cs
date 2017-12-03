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
        #region Model Objects
        Users objUsers = new Users();
        Purpose objPurpose = new Purpose();
        Booking objBooking = new Booking();
        #endregion

        #region User Login
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
            //bool isLoggedIn = objUsers.UserLogin(model);
            int userID = objUsers.UserLogin(model); //Verfies data from Model Users and fetches logged in userID
            if(userID!=0)
            {
                //Session["UserLoggedIn"] = 1;
                Session["LoggedInUserID"] = userID;
                return RedirectToAction("Index");
            }
            ViewBag.InvalidCredentials = "Invalid Credentials";
            return View();
        }
        #endregion

        #region User Home
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["LoggedInUserID"] != null)
            {
                ViewBag.purposeList = new SelectList(objPurpose.GetPurposeList(), "purposeID", "purpose"); //Fetches list of purpose for booking from Purpose Model
                return View();
            }
            return RedirectToAction("Login");
        }
        #region Make new Booking
        [HttpPost]
        public ActionResult Index(BigViewModel model)
        {
            int userID = Convert.ToInt32(Session["LoggedInUserID"]);
            int bookingID = objBooking.CreateNewBooking(model,userID); //Saves new booking by logged in user to DB (go to Booking Model) and fetches bookingID

            Session["NewBookingID"] = bookingID;

            ViewBag.purposeList = new SelectList(objPurpose.GetPurposeList(), "purposeID", "purpose"); //Fetches list of purpose for booking from Purpose Model

            return RedirectToAction("Testing", "BookUtility");
        }
        #endregion
        #endregion

        #region User Logout
        public ActionResult Logout()
        {
            Session["UserLoggedIn"] = null;
            return RedirectToAction("Login");
        }
        #endregion
    }
}