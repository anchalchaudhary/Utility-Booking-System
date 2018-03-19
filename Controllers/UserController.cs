using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityBookingSystem.Extra_Classes;
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
        BookedHall objBookedHall = new BookedHall();
        BookedDate objBookedDate = new BookedDate();
        BookedRequirement objBookedRequirement = new BookedRequirement();
        BookedSlot objBookedSlot = new BookedSlot();
        Chair objChair = new Chair();

        #endregion

        #region User Login
        public ActionResult Login()
        {
            //View All the booking without login

            List<Booking> allBookingsList = objBooking.GetAllBookingsList();
            ViewBag.allBookingsList = allBookingsList;

            List<BookedDate> bookingDateList = objBookedDate.GetAllBookingDateList();
            ViewBag.bookedDate = bookingDateList;

            List<BookedHall> allBookedHallsList = objBookedHall.GetAllBookedHalls();
            ViewBag.bookedHalls = allBookedHallsList;

            List<BookedSlot> allBookedSlotsList = objBookedSlot.GetBookedSlotsList(allBookedHallsList);
            ViewBag.bookedSlots = allBookedSlotsList;

            if (Session["LoggedInUserID"] != null)
            {
                return RedirectToAction("Index","User");
            }
            else if(Session["DeanAdminLoggedIn"]!=null)
            {
                return RedirectToAction("ViewBookingRequests", "Admin");
            }
            Session["LoggedInUserID"] = null;
            Session["DeanAdminLoggedIn"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(BigViewModel model)
        {
            int userID = objUsers.UserLogin(model); //Verfies data and fetches userID or 
            if (userID != 0)
            {
                if (model.login.loginID.ToUpper() == "DEAN ADMIN")
                {
                    Session["DeanAdminLoggedIn"] = userID;
                    return RedirectToAction("ViewBookingRequests", "Admin");
                }
                else
                {
                    Session["LoggedInUserID"] = userID;
                    return RedirectToAction("Index");
                }
            }

            ViewBag.InvalidCredentials = "Invalid Credentials";
            return View();
        }
        #endregion

        #region User Home
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["LoggedInUserID"] != null || Session["DeanAdminLoggedIn"] != null)
            {
                int userID=0;
                if (Session["LoggedInUserID"] != null)
                    userID = Convert.ToInt32(Session["LoggedInUserID"]);
                else if (Session["DeanAdminLoggedIn"] != null)
                    userID = Convert.ToInt32(Session["DeanAdminLoggedIn"]);
            
                ViewBag.purposeList = new SelectList(objPurpose.GetPurposeList(), "purposeID", "purpose"); //Fetches list of purpose for booking from Purpose Model

                List<Booking> listUserBooking = objBooking.getUserBookingDetails(userID);
                ViewBag.userBookings = listUserBooking;
                
                List<BookedDate> listUserFutureBookedDate = objBookedDate.getUserFutureBookedDate(listUserBooking);
                ViewBag.userFutureBookedDate = listUserFutureBookedDate;

                return View();
            }
            return RedirectToAction("Login");
        }

        #region View Application
        //[HttpGet]
        //public ActionResult ViewApplication(int bookingID, int userID)
        //{
        //    if (Session["LoggedInUserID"] != null || Session["DeanAdminLoggedIn"] != null)
        //    {
        //        Booking bookingDetails = objBooking.GetBookingDetails(bookingID);
        //        ViewBag.bookingDetails = bookingDetails;

        //        List<BookedDate> bookingDateList = objBookedDate.GetBookingDateList(bookingID);
        //        ViewBag.bookedDate = bookingDateList;

        //        List<BookedHall> bookedHallList = objBookedHall.GetBookedHallsList(bookingDateList);
        //        ViewBag.bookedHall = bookedHallList;

        //        List<Chair> chairsList = objChair.GetChairsList(bookingDateList, bookedHallList, bookingID);
        //        ViewBag.chairs = chairsList;

        //        List<BookedRequirement> bookedReqList = objBookedRequirement.GetBookedRequirementList(bookingID);
        //        ViewBag.bookedReq = bookedReqList;

        //        List<BookedSlot> bookingSlotList = objBookedSlot.GetBookingSlotsList(bookedHallList);
        //        ViewBag.bookingSlot = bookingSlotList;

        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}
        #endregion

        #region Make new Booking
        [HttpPost]
        public ActionResult Index(BigViewModel model)
        {
            int userID=0;
            if(Session["LoggedInUserID"] !=null)
                userID = Convert.ToInt32(Session["LoggedInUserID"]);
            else if(Session["DeanAdminLoggedIn"] != null)
                userID = Convert.ToInt32(Session["DeanAdminLoggedIn"]);

            int bookingID = objBooking.CreateNewBooking(model, userID); //Saves new booking and fetches bookingID

            Session["NewBookingID"] = bookingID;

            ViewBag.purposeList = new SelectList(objPurpose.GetPurposeList(), "purposeID", "purpose"); //Fetches list of purpose for booking

            return RedirectToAction("Testing", "BookUtility");
        }
        #endregion

        #region Cancel Booking
        [HttpGet]
        public ActionResult CancelBooking(int bookingID, int userID)
        {
            bool isCancelled = objBooking.CancelBooking(bookingID, userID);
            return RedirectToAction("Index", "User");
        }
        #endregion
        #endregion
        #region Change password
        public ActionResult AccountSettings()
        {
            if (Session["LoggedInUserID"] != null || Session["DeanAdminLoggedIn"] != null)
                return View();
            else if (Session["ChangePasswordUserID"] != null)
                return View();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult AccountSettings(Users model)
        {
            int userID = 0;
            if (Session["LoggedInUserID"] != null)
            {
                userID = Convert.ToInt32(Session["LoggedInUserID"]);
            }
            else if (Session["DeanAdminLoggedIn"] != null)
            {
                userID = Convert.ToInt32(Session["DeanAdminLoggedIn"]);
            }
            else if (Session["ChangePasswordUserID"] != null)
            {
                userID = Convert.ToInt32(Session["ChangePasswordUserID"]);
            }
            BookingSystemDBEntities db = new BookingSystemDBEntities();

            tblUser objtblUser = db.tblUsers.SingleOrDefault(x => x.userID == userID);
            if (objtblUser != null)
            {
                objtblUser.password = Encryption.Encrypt(model.password);
                db.SaveChanges();

                TempData["ChangedPassword"] = "<script src=\"https://unpkg.com/sweetalert/dist/sweetalert.min.js \"></script><script> swal('Your password was changed successfully.')</script>";
            }
            else
            {
                TempData["ChangedPassword"] = "<script src=\"https://unpkg.com/sweetalert/dist/sweetalert.min.js \"></script><script> swal('Some error occurred. Please try again')</script>";
            }
            if (Session["ChangePasswordUserID"] != null)
            {
                TempData["ChangedPassword"] = null;
                Session["ChangePasswordUserID"] = null;
                return RedirectToAction("Login");
            }

            return View();

        }
        #endregion
        public ActionResult Print(int bookingID)
        {
            int userID = 0;
            if (Session["LoggedInUserID"] != null)
                userID = Convert.ToInt32(Session["LoggedInUserID"]);
            else if (Session["DeanAdminLoggedIn"] != null)
                userID = Convert.ToInt32(Session["DeanAdminLoggedIn"]);

            Users userDetails = objUsers.GetUserDetails(userID);
            ViewBag.userDept = userDetails.dept;

            Booking bookingDetails = objBooking.GetBookingDetails(bookingID);
            ViewBag.bookingDetails = bookingDetails;

            List<BookedDate> bookingDateList = objBookedDate.GetBookingDateList(bookingID);
            ViewBag.bookedDate = bookingDateList;

            List<BookedHall> bookedHallList = objBookedHall.GetBookedHallsList(bookingDateList);
            ViewBag.bookedHall = bookedHallList;

            List<Chair> chairsList = objChair.GetChairsList(bookingDateList, bookedHallList, bookingID);
            ViewBag.chairs = chairsList;

            List<BookedRequirement> bookedReqList = objBookedRequirement.GetBookedRequirementList(bookingID);
            ViewBag.bookedReq = bookedReqList;

            List<BookedSlot> bookingSlotList = objBookedSlot.GetBookingSlotsList(bookedHallList);
            ViewBag.bookingSlot = bookingSlotList;

            return View();
        }
        #region User Logout
        public ActionResult Logout()
        {
            if (Session["DeanAdminLoggedIn"] != null)
                Session["DeanAdminLoggedIn"] = null;
            else if(Session["LoggedInUserID"]!=null)
                Session["LoggedInUserID"] = null;            
            return RedirectToAction("Login");
        }
        #endregion
        public ActionResult ForgotPassword()
        {
            Departments objDepartments = new Departments();
            ViewBag.deptList = new SelectList(objDepartments.GetDepartmentsList(), "deptID", "department");

            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(Users model)
        {
            Departments objDepartments = new Departments();
            ViewBag.deptList = new SelectList(objDepartments.GetDepartmentsList(), "deptID", "department");

            int userID = objUsers.getUserID(model.email, Convert.ToInt32(model.deptID));
            if (userID > 0)
            {
                string mailSubject, mailBody, changePasswordUrl;

                mailSubject = "Change Password";
                changePasswordUrl = "http://10.10.152.231/hbs/User/ResetPassword?usr=" + userID + "&email=" + Encryption.Encrypt(model.email);

                mailBody = "<a href='" + changePasswordUrl + "'>Click Here to change your password</a>";
                Mail.Send_Mail(model.email, mailBody, mailSubject); //Sends Mail to the registered User.

                TempData["ForgotMail"] = "<script src =\"https://unpkg.com/sweetalert/dist/sweetalert.min.js \"></script><script> swal('Link has been mailed to you.');</script>";
            }
            else
                TempData["ForgotMail"] = "<script src =\"https://unpkg.com/sweetalert/dist/sweetalert.min.js \"></script><script> swal('User does not exist.');</script>";
            return View();
        }
        public ActionResult ResetPassword()
        {
            string userID = Request.QueryString["usr"];
            int id = Convert.ToInt32(userID);

            Session["ChangePasswordUserID"] = id;
            return RedirectToAction("AccountSettings");
        }
    }
}