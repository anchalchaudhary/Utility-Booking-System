using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityBookingSystem.Extra_Classes;
using UtilityBookingSystem.Models;

namespace UtilityBookingSystem.Controllers
{
    public class BookUtilityController : Controller
    {
        // GET: BookUtility
        #region model classes
        Hall objHall = new Hall();
        Slot objSlot = new Slot();
        Users objUsers = new Users();
        RequirementForHall objRequirementForHall = new RequirementForHall();
        BookedDate objBookedDate = new BookedDate();
        BookedHall objBookedHall = new BookedHall();
        BookedSlot objBookedSlot = new BookedSlot();
        BookedRequirement objBookedRequirement = new BookedRequirement();
        Booking objBooking = new Booking();
        Chair objChair = new Chair();

        #endregion

        #region Start Booking
        public ActionResult Index() //not used
        {
            List<tblHall> listHall = new List<tblHall>();
            listHall = objHall.GetHallsList();
            ViewBag.HallDetail = listHall;
            return View();
        }
        [HttpPost]
        public ActionResult Index(List<DetailsList> detailsObjList) //not used
        {
            return RedirectToAction("Index");
        }
        #region Get Halls List
        //[HttpPost]
        public JsonResult GetHallsList()
        {
            List<tblHall> hallsList = objHall.GetHallsList();
            return Json(hallsList);
        }
        #endregion

        #region Get All Slots List
        public JsonResult GetAllSlotsList()
        {
            List<tblSlot> allSlotsList = objSlot.GetAllSlotsList();

            return Json(allSlotsList);
        }
        #endregion

        #region Get Booked Slots List
        [HttpPost]
        public JsonResult GetBookedSlotsList(DateTime date)
        {
            List<BookedSlot> bookedSlotsList = new List<BookedSlot>();
            List<BookedHall> bookedHallsList;
            List<BookedDate> bookedDateList;
            if (date != null)
            {
                bookedDateList = objBookedDate.GetBookedDateList(date);

                bookedHallsList = objBookedHall.GetBookedHallsList(bookedDateList);

                bookedSlotsList = objBookedSlot.GetBookedSlotsList(bookedHallsList);
            }
            return Json(bookedSlotsList);
        }
        #endregion

        #region Get Requirements List
        [HttpGet]
        public JsonResult GetRequirementsList()
        {
            List<RequirementForHall> requirementsList = new List<RequirementForHall>();
            requirementsList = objRequirementForHall.GetRequirementsList();
            return Json(requirementsList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Tooltip
        [HttpPost]
        public JsonResult GetBookedByDetails(DateTime date, int slotID, int hallID)
        {
            List<BookedSlot> listBookedHallID = objBookedSlot.GetBookedHallID(slotID);
            List<BookedHall> listBookedDateID = objBookedHall.GetBookedDateID(listBookedHallID, hallID);
            int bookingID = objBookedDate.GetBookingID(listBookedDateID, date);
            string department = objBooking.GetUserDetail(bookingID);

            return Json(department);
        }
        #endregion
        #endregion

        #region Booking step 2
        public ActionResult Testing()
        {
            if (Session["LoggedInUserID"] != null || Session["DeanAdminLoggedIn"]!=null)
            {
                if (Session["NewBookingID"] != null)
                {
                    List<Hall> listHall = new List<Hall>();
                    listHall = objHall.GetHallDetails();
                    ViewBag.HallDetail = listHall;
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }
                return View();
            }
            else
                return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public ActionResult Testing(List<DetailsList> detailsObjList)
        {

            List<Hall> listHall = new List<Hall>();
            listHall = objHall.GetHallDetails();
            ViewBag.HallDetail = listHall;

            int bookingID = Convert.ToInt32(Session["NewBookingID"]);
            int userID = 0;
            if(Session["LoggedInUserID"] != null)
                userID = Convert.ToInt32(Session["LoggedInUserID"]);
            else if(Session["DeanAdminLoggedIn"] != null)
                userID = Convert.ToInt32(Session["DeanAdminLoggedIn"]);

            bool slotFlag = false, reqFlag=false;
            foreach(var item in detailsObjList)
            {
                if(item.date!=null)
                {
                    foreach(var hallItem in item.hallsArray)
                    {
                        if(hallItem!=null)
                        {
                            foreach(var slotItem in hallItem.slotsArray)
                            {
                                if (slotItem.isSelected == true)
                                    slotFlag = true;
                            }
                            foreach(var reqItem in hallItem.requirementsArray)
                            {
                                if (reqItem.isSelected == true)
                                    reqFlag = true;
                            }
                            if (hallItem.otherRequirements != null)
                                reqFlag = true;
                        }
                    }
                }
            }
            if(slotFlag == false || reqFlag == false)
            {
                ViewBag.EnterAllDetails = "You must enter all details";
                return View();
            }

            foreach (var item in detailsObjList)
            {
                BookedHall objBookedHall1 = new BookedHall();
                //                int dateID = objBookedDate.SaveBookingDate(item.date, bookingID);

                int dateID = objBookedDate.SaveBookingDate(item, bookingID);

                objBookedHall1.SaveSelectedHalls(item.hallsArray, dateID, bookingID);
            }

            tblUser objtblUser = new tblUser();
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                objtblUser = context.tblUsers.First(x => x.userID == userID);
            }

            tblBooking objtblBooking = new tblBooking();
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                objtblBooking = context.tblBookings.First(x => x.bookingID == bookingID);
            }
            string mailSubject, mailBody;

            mailSubject = "Request Sent";
            mailBody = objtblUser.name + ", your booking request has been sent. You\'re booking number is " + objtblBooking.bookingNo;
            Mail.Send_Mail(objtblUser.email, mailBody, mailSubject); //Sends Mail to the registered User.

            return RedirectToAction("Print");
        }
        #endregion
        #region View Application
        [HttpGet]
        //public ActionResult ViewApplication()
        //{
        //    if (Session["LoggedInUserID"] != null || Session["DeanAdminLoggedIn"] != null)
        //    {
        //        if (Session["NewBookingID"] != null)
        //        {
        //            TempData["Requested"] = "<script src=\"https://unpkg.com/sweetalert/dist/sweetalert.min.js \"></script><script> swal('Your booking requested has been sent. Please check your email.')</script>";

        //            int bookingID = Convert.ToInt32(Session["NewBookingID"]);

        //            Booking bookingDetails = objBooking.GetBookingDetails(bookingID);
        //            ViewBag.bookingDetails = bookingDetails;

        //            List<BookedDate> bookingDateList = objBookedDate.GetBookingDateList(bookingID);
        //            ViewBag.bookedDate = bookingDateList;

        //            List<BookedHall> bookedHallList = objBookedHall.GetBookedHallsList(bookingDateList);
        //            ViewBag.bookedHall = bookedHallList;

        //            List<Chair> chairsList = objChair.GetChairsList(bookingDateList, bookedHallList, bookingID);
        //            ViewBag.chairs = chairsList;

        //            List<BookedRequirement> bookedReqList = objBookedRequirement.GetBookedRequirementList(bookingID);
        //            ViewBag.bookedReq = bookedReqList;

        //            List<BookedSlot> bookingSlotList = objBookedSlot.GetBookingSlotsList(bookedHallList);
        //            ViewBag.bookingSlot = bookingSlotList;

        //            Session["NewBookingID"] = null;
        //        }
        //        else
        //        {
        //            return RedirectToAction("Testing");
        //        }
        //        return View();
        //    }
        //    else
        //        return RedirectToAction("Login", "User");
        //}
    #endregion 
        #region Print
        public ActionResult Print()
        {
            int bookingID = Convert.ToInt32(Session["NewBookingID"]);
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
        #endregion
    }
}