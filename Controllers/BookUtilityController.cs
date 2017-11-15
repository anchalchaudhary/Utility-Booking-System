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
        RequirementForHall objRequirementForHall = new RequirementForHall();
        BookedDate objBookedDate = new BookedDate();
        BookedHall objBookedHall = new BookedHall();
        BookedSlot objBookedSlot = new BookedSlot();
        BookedRequirement objBookedRequirement = new BookedRequirement();
        #endregion

        #region Start Booking
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(int[] hallsArray, int[] requirementsArray, int[] slotsArray, DateTime date)
        {
            if (date != null)
            {
                int bookingID = Convert.ToInt32(Session["NewBookingID"]);

                int dateID = objBookedDate.SaveBookingDate(date, bookingID);

                objBookedHall.SaveSelectedHalls(hallsArray, dateID);

                objBookedRequirement.SaveSelectedRequirements(hallsArray, requirementsArray, bookingID, dateID);
            }
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
        
        #region Get Requirements List
        //[HttpPost]
        public JsonResult GetRequirementsList(int hallID)
        {
            List<RequirementForHall> requirementsList = objRequirementForHall.GetRequirementsList(hallID);
            return Json(requirementsList);
        }
        #endregion

        #region Get All Slots List
        public JsonResult GetAllSlotsList()
        {
            List<tblSlot> allSlotsList = objSlot.GetAllSlotsList();

            return Json(allSlotsList);
        }
        #endregion

        #region Get Slots List
        public JsonResult GetBookedSlotsList(int hallID, DateTime date)
        {
            List<BookedDate> bookedDateList = objBookedDate.GetBookedDateList(date);

            List<BookedHall> bookedHallsList = objBookedHall.GetBookedHallsList(bookedDateList,hallID);

            List<BookedSlot> bookedSlotsList = objBookedSlot.GetBookedSlotsList(bookedHallsList);

            return Json(bookedSlotsList);
        }
        #endregion

        #endregion
    }
}