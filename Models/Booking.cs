using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Repository;

namespace UtilityBookingSystem.Models
{
    public class Booking
    {
        #region tblBooking attributes
        public int bookingID { get; set; }
        public Nullable<int> userID { get; set; }
        public Nullable<int> purposeID { get; set; }
        public string title { get; set; }
        public string purpose { get; set; }
        #endregion
        public string name { get; set; }
        public string department { get; set; }
        #region Repository objects
        UserPanelRepository objUserPanelRepository = new UserPanelRepository();
        BookingRepository objBookingRepository = new BookingRepository();
        #endregion

        #region Creates new Booking
        public int CreateNewBooking(BigViewModel model, int userID)
        {
            Booking objBooking = new Booking();
            objBooking.title = model.booking.title;
            objBooking.purposeID = model.booking.purposeID;
            objBooking.userID = userID;

            int bookingID= objBookingRepository.CreateNewBooking(objBooking); //Saves new Booking details to db (Booking repository) and fetches new bookingID

            return bookingID;
        }
        #endregion

        #region Get All Bookings List
        public List<Booking> GetAllBookingsList()
        {
            List<Booking> allBookingsList;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                allBookingsList = context.tblBookings.Select(x => new Booking
                {
                    bookingID = x.bookingID,
                    userID = x.userID,
                    name = x.tblUser.name,
                    department = x.tblUser.tblDepartment.department,
                    title = x.title,
                    purpose = x.tblPurpose.purpose,
                }).ToList();
            }
            return allBookingsList;
        }
        #endregion
    }
}