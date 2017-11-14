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
        public string slot { get; set; }
        public bool isChecked { get; set; }

        #region Get Booked Slots
        public List<BookedSlot> GetBookedSlotsList(List<BookedHall> bookedHallsList)
        {
            List<BookedSlot> bookedSlotsList = new List<BookedSlot>();
            List<BookedSlot> availableSlotsList = new List<BookedSlot>();
            List<BookedSlot> allSlotsList = new List<BookedSlot>();

            IEnumerable<BookedSlot> bookedSlots = null;
            IEnumerable<BookedSlot> availableSlots = null;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                foreach (var item in bookedHallsList)
                {
                    bookedSlots = context.tblBookedSlots.Where(x => x.bookedHallID == item.bookedHallID).Select(x => new BookedSlot
                    {
                    //    bookedSlotID = x.bookedSlotID,
                    //    bookedHallID = x.bookedHallID,
                        slotID = x.slotID,
                        isChecked = false,
                        slot = x.tblSlot.slot
                    }).ToList();
                    bookedSlotsList.AddRange(bookedSlots);
                }
                allSlotsList = bookedSlotsList;
                //foreach(var item in bookedSlotsList)
                //{
                //    availableSlots = context.tblSlots.Where(x => x.slotID != item.slotID).Select(x => new BookedSlot
                //    {
                //        slotID = x.slotID,
                //        isChecked = true
                //    }).ToList();
                //    availableSlotsList.AddRange(availableSlots);
                //}
                //allSlotsList.AddRange(availableSlots);
            }
            return allSlotsList;
        }
        #endregion
    }
}