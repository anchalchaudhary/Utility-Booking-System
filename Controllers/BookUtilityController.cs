﻿using System;
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
        Requirement objRequirement = new Requirement();
        BookedDate objBookedDate = new BookedDate();
        BookedHall objBookedHall = new BookedHall();
        BookedSlot objBookedSlot = new BookedSlot();
        #endregion
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(BigViewModel model)
        {
            return View();
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
            List<Requirement> requirementsList = objRequirement.GetRequirementsList(hallID);
            return Json(requirementsList);
        }
        #endregion

        #region Get Slots List
        public JsonResult GetSlotsList(int hallID, DateTime date)
        {
            List<BookedDate> bookedDateList = objBookedDate.GetBookedDateList(date);

            List<BookedHall> bookedHallsList = objBookedHall.GetBookedHallsList(bookedDateList);

            List<BookedSlot> bookedSlotsList = objBookedSlot.GetBookedSlotsList(bookedHallsList);

            return Json(bookedSlotsList);
        }
        #endregion
    }
}