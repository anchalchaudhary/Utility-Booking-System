using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Repository;

namespace UtilityBookingSystem.Models
{
    public class BookedRequirement
    {
        #region tblBookedRequirement attributes
        public int bookedReqID { get; set; }
        public Nullable<int> bookingID { get; set; }
        public Nullable<int> hallID { get; set; }
        public Nullable<int> dateID { get; set; }
        #endregion
        public string bookedReq { get; set; }
        public string hallName { get; set; }

        BookingRepository objBookingRepository = new BookingRepository();

        #region Save selected requirements
        public void SaveSelectedRequirements(int[] hallsArray, int[] requirementsArray, int[] slotsArray, int bookingID, int dateID)
        {
            objBookingRepository.SaveSelectedRequirements(hallsArray, requirementsArray, bookingID, dateID);
        }
        #endregion
        #region Booked Requirements
        public List<BookedRequirement> GetBookedRequirementList(int bookingID)
        {
            List<BookedRequirement> bookedReqList;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;

                bookedReqList = context.tblBookedRequirements.Where(x => x.bookingID == bookingID).Select(x => new BookedRequirement
                {
                    bookingID = x.bookingID,
                    bookedReqID = x.bookedReqID,
                    bookedReq = x.tblRequirementForHall.tblRequirement.requirementName,
                    hallID = x.reqHallID,
                    hallName = x.tblRequirementForHall.tblHall.hallName,
                    dateID = x.dateID
                }).ToList();
            }
            return bookedReqList;
        }
        #endregion
    }
}