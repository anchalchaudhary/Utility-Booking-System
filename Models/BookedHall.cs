using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class BookedHall
    {
        #region tblBookedHall attributes
        public int bookedHallID { get; set; }
        public Nullable<int> hallID { get; set; }
        public Nullable<int> dateID { get; set; }
        #endregion
        #region Get Booked Halls List
        public List<BookedHall> GetBookedHallsList(List<BookedDate> bookedDateList, int hallID)
        {
            IEnumerable<BookedHall> bookedHalls =null;
            List<BookedHall> bookedHallsList = new List<BookedHall>();
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                // context.Configuration.ProxyCreationEnabled = true;
                foreach (var item in bookedDateList)
                {
                    bookedHalls = context.tblBookedHalls.Where(x => x.dateID == item.dateID && x.hallID==hallID).Select(x => new BookedHall
                    {
                        bookedHallID = x.bookedHallID,
                        hallID = x.hallID,
                        dateID = x.dateID
                    }).ToList();
                    bookedHallsList.AddRange(bookedHalls); 
                }
            }
            return bookedHallsList;
        }
        #endregion
    }
}