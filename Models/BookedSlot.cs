using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class BookedSlot
    {
        #region tblBookedSlot attributes
        public int bookedSlotID { get; set; }
        public Nullable<int> bookedHallID { get; set; }
        public Nullable<int> slotID { get; set; }
        #endregion

        #region Get Booked Slots
        public List<BookedSlot> GetBookedSlotsList(List<BookedHall> bookedHallsList)
        {
            List<BookedSlot> bookedSlotsList = new List<BookedSlot>();
            IEnumerable<BookedSlot> bookedSlots = null;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                foreach (var item in bookedHallsList)
                {
                    bookedSlots = context.tblBookedSlots.Where(x => x.bookedHallID == item.bookedHallID).Select(x => new BookedSlot
                    {
                    //    bookedSlotID = x.bookedSlotID,
                    //    bookedHallID = x.bookedHallID,
                        slotID = x.slotID
                    }).ToList();
                    bookedSlotsList.AddRange(bookedSlots);
                }
            }
            return bookedSlotsList;
        }
        #endregion
    }
}