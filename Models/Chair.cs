using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class Chair
    {
        public int chairID { get; set; }
        public Nullable<int> noOfChairs { get; set; }
        public Nullable<int> hallID { get; set; }
        public Nullable<int> bookingID { get; set; }
        public Nullable<int> dateID { get; set; }

        public List<Chair> GetChairsList(List<BookedDate> bookingDateList, List<BookedHall> bookedHallList, int bookingID)
        {
            IEnumerable<Chair> chairList = null;
            List<Chair> noOfChairList = new List<Chair>();
            using (var db = new BookingSystemDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                foreach (var dateItem in bookingDateList)
                {
                    foreach (var hallItem in bookedHallList)
                    {
                        chairList = db.tblChairs.Where(x => x.hallID == hallItem.hallID && x.bookingID == bookingID && x.dateID==dateItem.dateID).Select(x => new Chair
                        {
                            noOfChairs = x.noOfChairs,
                            hallID = x.hallID,
                            bookingID = x.bookingID,
                            dateID = x.dateID
                        }).ToList();
                        noOfChairList.AddRange(chairList);

                    }
                }
            }
            return noOfChairList;
        }
    }
}