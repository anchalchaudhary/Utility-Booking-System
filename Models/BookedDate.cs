using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public Nullable<bool> nonWorkingHours { get; set; }
        public Nullable<bool> annexeRequired { get; set; }
        public Nullable<bool> photographer { get; set; }

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
        public List<BookedDate> GetAllBookingDateList()
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
        //public int SaveBookingDate(DateTime date, int bookingID)
        public int SaveBookingDate(DetailsList item, int bookingID)
        {
            int dateID = objBookingRepository.SaveBookingDate(item, bookingID);
            return dateID;
        }
        #endregion
        #region Get Booking's Dates
        public List<BookedDate> GetBookingDateList(int bookingID)
        {
            List<BookedDate> bookedDateList = new List<BookedDate>();
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                bookedDateList = context.tblBookedDates.Where(x => x.bookingID == bookingID).Select(x => new BookedDate
                {
                    bookingID = x.bookingID,
                    dateID = x.dateID,
                    dateChosen = x.dateChosen,
                    nonWorkingHours = x.nonWorkingHours,
                    annexeRequired = x.annexeRequired,
                    photographer = x.photographer
                }).ToList();
            }
            return bookedDateList;
        }
        #endregion

        #region Get Booking ID
        public int GetBookingID(List<BookedHall> listBookedDateID, DateTime date)
        {
            int bookingID=0;
            DateTime dateChosen = date.Date;
            tblBookedDate objtblBookedDate = new tblBookedDate();
            List<tblBookedDate> listtblBookedDate = new List<tblBookedDate>();
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                listtblBookedDate = context.tblBookedDates.ToList();
                foreach(var item in listBookedDateID)
                {
                    foreach(var itemtbl in listtblBookedDate)
                    {
                        if(itemtbl.dateID == item.dateID && itemtbl.dateChosen == dateChosen)
                        {
                            bookingID = Convert.ToInt32(itemtbl.bookingID);
                            break;
                        }
                    }
                    if (bookingID != 0)
                        break;
                }
               // bookingID = Convert.ToInt32(objtblBookedDate.bookingID);
            }
            return bookingID;
        }
        #endregion
        public List<BookedDate> getUserPastBookedDate(List<Booking> listUserPastBookedDate)
        {
            IEnumerable<BookedDate> listBookedDate = null;
            List<BookedDate> userBookedDateList = new List<BookedDate>();
            DateTime dateToday = DateTime.Now.Date;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                foreach(var item in listUserPastBookedDate)
                {
                    listBookedDate = context.tblBookedDates.Where(x => x.bookingID == item.bookingID && x.dateChosen< dateToday).Select(x => new BookedDate
                    {
                        dateID = x.dateID,
                        dateChosen = x.dateChosen,
                        bookingID = x.bookingID
                    }).ToList();
                    userBookedDateList.AddRange(listBookedDate);
                }
            }
            return userBookedDateList;
        }
        public List<BookedDate> getUserFutureBookedDate(List<Booking> listUserPastBookedDate)
        {
            IEnumerable<BookedDate> listBookedDate = null;
            List<BookedDate> userBookedDateList = new List<BookedDate>();
            DateTime dateToday = DateTime.Now.Date;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                foreach (var item in listUserPastBookedDate)
                {
                    listBookedDate = context.tblBookedDates.Where(x => x.bookingID == item.bookingID && x.dateChosen >= dateToday).Select(x => new BookedDate
                    {
                        dateID = x.dateID,
                        dateChosen = x.dateChosen,
                        bookingID = x.bookingID
                    }).ToList();
                    userBookedDateList.AddRange(listBookedDate);
                }
            }
            return userBookedDateList;
        }

    }
}