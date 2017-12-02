using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Repository;


namespace UtilityBookingSystem.Models
{
    public class BookedHall
    {
        #region tblBookedHall attributes
        public int bookedHallID { get; set; }
        public Nullable<int> hallID { get; set; }
        public Nullable<int> dateID { get; set; }
        #endregion

        public string hallName { get; set; }

        BookingRepository objBookingRepository = new BookingRepository();

        #region Get Booked Halls List
        public List<BookedHall> GetBookedHallsList(List<BookedDate> bookedDateList)
        {
            IEnumerable<BookedHall> bookedHalls =null;
            List<BookedHall> bookedHallsList = new List<BookedHall>();
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                // context.Configuration.ProxyCreationEnabled = true;
                foreach (var item in bookedDateList)
                {
                    bookedHalls = context.tblBookedHalls.Where(x => x.dateID == item.dateID).Select(x => new BookedHall
                    {
                        bookedHallID = x.bookedHallID,
                        hallID = x.hallID,
                        dateID = x.dateID,
                        hallName = x.tblHall.hallName
                    }).ToList();
                    bookedHallsList.AddRange(bookedHalls); 
                }
            }
            return bookedHallsList;
        }
        #endregion

        #region Save Selected Hall
        public void SaveSelectedHalls(List<Hall> hallsArray, int dateID, int bookingID)
        {
            objBookingRepository.SaveSelectedHalls(hallsArray, dateID, bookingID);
        }
        #endregion
    }
}