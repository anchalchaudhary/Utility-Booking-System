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

        #region Save Booking Date
        public int SaveBookingDate(DateTime date, int bookingID)
        {
            tblBookedDate objtblBookedDate = new tblBookedDate();

            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                objtblBookedDate.bookingID = bookingID;
                objtblBookedDate.dateChosen = date;

                db.tblBookedDates.Add(objtblBookedDate);

                db.SaveChanges();
            }
            int dateID = objtblBookedDate.dateID;
            return dateID;
        }
        #endregion

        #region Save Selected Hall
        public void SaveSelectedHalls(int[] hallsArray, int dateID)
        {
            int i;
            tblBookedHall objtblBookedHall = new tblBookedHall();
            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                for(i=0;i<hallsArray.Length;i++)
                {
                    objtblBookedHall.hallID = hallsArray[i];
                    objtblBookedHall.dateID = dateID;

                    db.tblBookedHalls.Add(objtblBookedHall);

                    db.SaveChanges();
                }
            }
        }
        #endregion

        #region Save selected Requirements
        public void SaveSelectedRequirements(int[] hallsArray, int[] requirementsArray, int bookingID, int dateID)
        {
            int i;
            tblBookedRequirement objtblBookedRequirement = new tblBookedRequirement();

            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                for (i = 0; i < requirementsArray.Length; i++)
                {
                    //objtblRequirementForHall.reqHallID = requirementsArray[i];

                    objtblBookedRequirement.dateID = dateID;
                    objtblBookedRequirement.bookingID = bookingID;
                    objtblBookedRequirement.reqHallID = requirementsArray[i];

                    db.tblBookedRequirements.Add(objtblBookedRequirement);
                    db.SaveChanges();
                }
            }
        }
        #endregion
    }
}