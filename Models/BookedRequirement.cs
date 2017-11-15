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

        BookingRepository objBookingRepository = new BookingRepository();

        #region Save selected requirements
        public void SaveSelectedRequirements(int[] hallsArray, int[] requirementsArray, int bookingID, int dateID)
        {
            objBookingRepository.SaveSelectedRequirements(hallsArray, requirementsArray, bookingID, dateID);
        }
        #endregion
    }
}