using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Repository;

namespace UtilityBookingSystem.Models
{
    public class BookedDate
    {
        #region tblBookedDate attributes
        public int dateID { get; set; }
        public Nullable<System.DateTime> dateChosen { get; set; }
        public Nullable<int> bookingID { get; set; }
        #endregion

        BookingRepository objBookingRepository = new BookingRepository();

        #region Get Booked Date List
        public List<BookedDate> GetBookedDateList(DateTime date)
        {
            List<BookedDate> bookedDateList;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                bookedDateList = context.tblBookedDates.Where(x => x.dateChosen == date).Select(x => new BookedDate
                {
                    dateID = x.dateID
                }).ToList();
            }
            return bookedDateList;
        }
        #endregion
        #region Save new Date
        public int SaveBookingDate(DateTime date, int bookingID)
        {
            int dateID = objBookingRepository.SaveBookingDate(date, bookingID);
            return dateID;
        }
        #endregion
    }
}