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
        public Nullable<int> status { get; set; }
        public string bookingNo { get; set; }
        public Nullable<System.DateTime> bookedAtTime { get; set; }

        #endregion
        public string name { get; set; }
        public string department { get; set; }
        #region Repository objects
        UserPanelRepository objUserPanelRepository = new UserPanelRepository();
        BookingRepository objBookingRepository = new BookingRepository();
        AdminPanelRepository objAdminPanelRepository = new AdminPanelRepository();
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
                    objtblBooking.status = 1;
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
                    objtblBooking.status = 0;
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

        public Booking GetBookingDetails(int bookingID)
        {
            Booking bookingDetail = new Booking();
            tblBooking objtblBooking = new tblBooking();
            tblPurpose objtblPurpose = new tblPurpose();
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                objtblBooking = context.tblBookings.First(x => x.bookingID == bookingID);
            }
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                objtblPurpose = context.tblPurposes.First(x => x.purposeID == objtblBooking.purposeID);
            }
            bookingDetail.bookingNo = objtblBooking.bookingNo;
            bookingDetail.bookingID = objtblBooking.bookingID;
            bookingDetail.title = objtblBooking.title;
            bookingDetail.bookedAtTime = objtblBooking.bookedAtTime;
            bookingDetail.purpose = objtblPurpose.purpose;
            bookingDetail.status = objtblBooking.status;

            return bookingDetail;
        }
        public string GetUserDetail(int bookingID)
        {
            if (bookingID != 0)
            {
                Booking objBooking = new Booking();
                Users objUser = new Users();
                tblBooking objtblBooking = new tblBooking();
                using (var context = new BookingSystemDBEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    objtblBooking = context.tblBookings.FirstOrDefault(x => x.bookingID == bookingID);
                }
                int userID = Convert.ToInt32(objtblBooking.userID);
                tblUser objtblUser = new tblUser();
                tblDepartment objtblDepartment = new tblDepartment();
                using (var context = new BookingSystemDBEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    objtblUser = context.tblUsers.First(x => x.userID == userID);
                }
                using (var context = new BookingSystemDBEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    objtblDepartment = context.tblDepartments.First(x => x.deptID == objtblUser.deptID);
                }
                string department = objtblDepartment.department;
                return department;
            }
            else
                return "";
        }
        public List<Booking> getUserBookingDetails(int userID)
        {
            List<Booking> listUserBooking = new List<Booking>();
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                listUserBooking = context.tblBookings.Where(x => x.userID == userID).Select(x=>new Booking
                {
                    bookingID=x.bookingID,
                    userID = x.userID,
                    bookingNo = x.bookingNo,
                    title = x.title,
                    bookedAtTime = x.bookedAtTime,
                    status = x.status
                }).ToList();
            }
            return listUserBooking;
        }

        public bool CancelBooking(int bookingID, int userID)
        {
            bool isCancelled = objUserPanelRepository.CancelBooking(bookingID, userID);
            return isCancelled;
        }
        public bool DeleteBooking(int bookingID)
        {
            bool isDeleted = objAdminPanelRepository.DeleteBooking(bookingID);
            return isDeleted;
        }
    }
}