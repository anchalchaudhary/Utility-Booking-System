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
        //public List<BookedHall> bookedHalls { get; set; }
        //public List<BookedSlot> bookedSlots { get; set; }
        //public List<BookedRequirement> bookedRequirements { get; set; }
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
        #region Get All Booked Dates
        public List<BookedDate> GetAllBookedDatesList()
        {
            List<BookedDate> allBookedDateList;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                allBookedDateList = context.tblBookedDates.Select(x => new BookedDate
                {
                    bookingID = x.bookingID,
                    dateChosen = x.dateChosen,
                    dateID = x.dateID
                }).ToList();
            }
            return allBookedDateList;
        }
        #endregion
        #region Save new Date
        public int SaveBookingDate(DateTime date, int bookingID)
        {
            int dateID = objBookingRepository.SaveBookingDate(date, bookingID);
            return dateID;
        }
        #endregion
        #region Get Booking's Dates
        public List<BookedDate> GetBookingDateList(int bookingID)
        {
            List<BookedDate> bookedDateList = new List<BookedDate>();
            //List<tblBookedHall> listtblBookedHall = new List<tblBookedHall>();
            //List<tblBookedSlot> listtblBookedSlot = new List<tblBookedSlot>();
            //List<tblBookedRequirement> listtblBookedRequirement = new List<tblBookedRequirement>();
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                bookedDateList = context.tblBookedDates.Where(x => x.bookingID == bookingID).Select(x => new BookedDate
                {
                    bookingID = x.bookingID,
                    dateID = x.dateID,
                    dateChosen = x.dateChosen
                }).ToList();
                //listtblBookedSlot = context.tblBookedSlots.ToList();
                //listtblBookedHall = context.tblBookedHalls.ToList();
                //listtblBookedRequirement = context.tblBookedRequirements.ToList();
            }
            //foreach (var item in bookedDateList)
            //{
            //    item.bookedSlots = new List<BookedSlot>();
            //    foreach (var slotItem in listtblBookedSlot)
            //    {
            //        item.bookedSlots.Add(new BookedSlot()
            //        {
            //            slotID = slotItem.tblSlot.slotID,
            //            slot = slotItem.tblSlot.slot,
            //            hallID = slotItem.tblBookedHall.hallID,
            //        });
            //    }
            //}
            //foreach (var item in bookedDateList)
            //{
            //    item.bookedHalls = new List<BookedHall>();
            //    foreach (var hallItem in listtblBookedHall)
            //    {
            //        item.bookedHalls.Add(new BookedHall()
            //        {
            //            dateID=hallItem.dateID,
            //            hallID = hallItem.hallID,
            //            hallName = hallItem.tblHall.hallName
            //        });
            //    }
            //}
            return bookedDateList;
        }
        #endregion
    }
}