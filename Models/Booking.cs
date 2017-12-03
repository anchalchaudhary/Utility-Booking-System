using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Extra_Classes;
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
        public Nullable<bool> status { get; set; }
        public string bookingNo { get; set; }
        public Nullable<System.DateTime> bookedAtTime { get; set; }

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

            int bookingID = objBookingRepository.CreateNewBooking(objBooking); //Saves new Booking details to db (Booking repository) and fetches new bookingID

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
                    bookingNo = x.bookingNo,
                    userID = x.userID,
                    name = x.tblUser.name,
                    department = x.tblUser.tblDepartment.department,
                    title = x.title,
                    purpose = x.tblPurpose.purpose,
                    bookedAtTime = x.bookedAtTime,
                    status = x.status
                }).ToList();
            }
            return allBookingsList;
        }
        #endregion

        #region Approve Booking
        public bool ApproveBooking(int bookingID)
        {
            string email = "";
            bool result = false;
            using (var context = new BookingSystemDBEntities())
            {
                tblBooking objtblBooking = context.tblBookings.SingleOrDefault(x => x.bookingID == bookingID);
                if (objtblBooking != null)
                {
                    result = true;
                    objtblBooking.status = true;
                    //}
                    context.SaveChanges();
                    //if (objtblBooking != null)
                    //{
                    email = objtblBooking.tblUser.email;
                    string body = "Hello, " + objtblBooking.tblUser.name + "! Your booking has been approved.";
                    string subject = "Booking for " + objtblBooking.title + " approved.";

                    //  Mail.Send_Mail(email, body, subject);
                }
                return result;
            }
        }
        public bool UnapproveBooking(int bookingID)
        {
            string email = "";
            bool result = false;
            using (var context = new BookingSystemDBEntities())
            {
                tblBooking objtblBooking = context.tblBookings.SingleOrDefault(x => x.bookingID == bookingID);
                if (objtblBooking != null)
                {
                    result = true;
                    objtblBooking.status = false;
                    //}
                    context.SaveChanges();
                    //if (objtblBooking != null)
                    //{
                    email = objtblBooking.tblUser.email;
                    string body = "Hello, " + objtblBooking.tblUser.name + "! Your booking has been Unapproved.";
                    string subject = "Booking for " + objtblBooking.title + " Unapproved.";

                    //  Mail.Send_Mail(email, body, subject);
                }
                return result;
            }
        }
        #endregion
    }
}