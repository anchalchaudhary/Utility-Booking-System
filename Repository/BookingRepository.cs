using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Models;

namespace UtilityBookingSystem.Repository
{
    public class BookingRepository
    {
        #region Save new booking details to DB
        public int CreateNewBooking(Booking objBooking) //Saves booking details
        {
            int bookingID;
            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                tblBooking objtblBooking = new tblBooking();
                objtblBooking.title = objBooking.title;
                objtblBooking.purposeID = objBooking.purposeID;
                objtblBooking.userID = objBooking.userID;

                db.tblBookings.Add(objtblBooking);
                db.SaveChanges();

                bookingID = objtblBooking.bookingID; //fetches bookingID of new Booking
            }
            return bookingID;
        }
        #endregion
    }
}