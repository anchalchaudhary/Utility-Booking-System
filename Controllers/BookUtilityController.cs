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
        Hall objHall = new Hall();
        Requirement objRequirement = new Requirement();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(BigViewModel model)
        {
            return View();
        }
        //[HttpPost]
        public JsonResult GetHallsList()
        {
            List<tblHall> hallsList = objHall.GetHallsList();
            return Json(hallsList);
        }
        public JsonResult GetRequirementsList(int hallID)
        {
            List<Requirement> requirementsList = objRequirement.GetRequirementsList(hallID);
            return Json(requirementsList);
        }
    }
}