using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Models;

namespace UtilityBookingSystem.Repository
{
    public class UserPanelRepository
    {
        #region Verify User Login Details
        public int UserLogin(Users objUser)
        {
            //bool isLoggedIn = false;
            int userID=0;
            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                tblUser objtblUser = db.tblUsers.SingleOrDefault(x => x.email == objUser.email && x.password == objUser.password); //Verifies Login details with DB
                if (objtblUser != null)
                {
                    //isLoggedIn = true;
                    userID = objtblUser.userID; //fetches logged in userID
                }
            }
            return userID;
        }
        #endregion
        public bool CancelBooking(int bookingID, int userID)
        {
            try
            {
                tblBooking objtblBooking = new tblBooking();
                using (BookingSystemDBEntities db = new BookingSystemDBEntities())
                {
                    objtblBooking = db.tblBookings.First(x => x.bookingID == bookingID && x.userID==userID);
                }
                using (BookingSystemDBEntities db = new BookingSystemDBEntities())
                {
                    db.tblBookings.Attach(objtblBooking);
                    db.tblBookings.Remove(objtblBooking);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}