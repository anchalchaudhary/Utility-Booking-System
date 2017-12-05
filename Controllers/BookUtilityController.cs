using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        #endregion

        #region Start Booking
        public ActionResult Index()
        {
            List<tblHall> listHall = new List<tblHall>();
            listHall = objHall.GetHallsList();
            ViewBag.HallDetail = listHall;
            return View();
        }
        [HttpPost]
        public ActionResult Index(List<DetailsList> detailsObjList)
        {
            //if (detailsObjList != null)
            //{
            //    foreach (var item in detailsObjList)
            //    {
            //        if (item.date >= DateTime.Now)
            //        {
            //            int bookingID = Convert.ToInt32(Session["NewBookingID"]);

            //            int dateID = objBookedDate.SaveBookingDate(item.date, bookingID);

            //            objBookedHall.SaveSelectedHalls(item.hallsArray, dateID);

            //            objBookedRequirement.SaveSelectedRequirements(item.hallsArray, item.requirementsArray, item.slotsArray, bookingID, dateID);
            //        }
            //    }
       // }
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
        //[HttpPost]
        //public JsonResult GetBookedByDetails(DateTime date)
        //{
        //    BookedDate objBookedDateDetails = objBookedDate.GetBookedDateList(date);
        //    tblBooking objtblBooking
        //    List<Users> userList = new List<Users>();
        //    userList = objUsers.GetUserDetails()
        //}
        #endregion
        #endregion

        #region TESTING
        public ActionResult Testing()
        {
            List<Hall> listHall = new List<Hall>();
            listHall = objHall.GetHallDetails();
            ViewBag.HallDetail = listHall;
            return View();
        }
        [HttpPost]
        public ActionResult Testing(List<DetailsList> detailsObjList)
        {
            int bookingID = Convert.ToInt32(Session["NewBookingID"]);

            List<Hall> listHall = new List<Hall>();
            listHall = objHall.GetHallDetails();
            ViewBag.HallDetail = listHall;

            foreach (var item in detailsObjList)
            {
                BookedHall objBookedHall1 = new BookedHall();
                int dateID = objBookedDate.SaveBookingDate(item.date, bookingID);
                objBookedHall1.SaveSelectedHalls(item.hallsArray, dateID, bookingID);
            }

            return View();
        }
            #endregion
        }
}