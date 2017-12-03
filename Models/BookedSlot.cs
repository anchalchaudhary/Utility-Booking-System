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

        public Nullable<int> hallID { get; set; }
        public string hallName { get; set; }
        public string slot { get; set;}

        #region added attributes
        public bool isChecked { get; set; }
        #endregion

        #region Get Booked Slots
        public List<BookedSlot> GetBookedSlotsList(List<BookedHall> bookedHallsList)
        {
            List<BookedSlot> bookedSlotsList = new List<BookedSlot>();
            List<BookedSlot> allSlotsList = new List<BookedSlot>();

            IEnumerable<BookedSlot> bookedSlots = null;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                foreach (var item in bookedHallsList)
                {
                    bookedSlots = context.tblBookedSlots.Where(x => x.bookedHallID == item.bookedHallID).Select(x => new BookedSlot
                    {
                        bookedSlotID = x.bookedSlotID,
                        bookedHallID = x.bookedHallID,
                        hallID = x.tblBookedHall.hallID,
                        slotID = x.slotID,
                        isChecked = false,
                        slot = x.tblSlot.slot
                    }).ToList();
                    bookedSlotsList.AddRange(bookedSlots);
                }
                allSlotsList = bookedSlotsList;
            }
            return allSlotsList;
        }
        #endregion

        #region Booking's Slot
        public List<BookedSlot> GetBookingSlotsList(List<BookedHall> bookedHallList)
        {
            List<BookedSlot> bookingSlotList = new List<BookedSlot>();
            List<BookedSlot> allSlotsList = new List<BookedSlot>();

            IEnumerable<BookedSlot> bookedSlots = null;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                foreach(var item in bookedHallList)
                {
                    bookedSlots = context.tblBookedSlots.Where(x => x.bookedHallID == item.bookedHallID).Select(x=> new BookedSlot
                    {
                        bookedHallID = x.bookedHallID,
                        slotID = x.slotID,
                        hallName = x.tblBookedHall.tblHall.hallName,
                        slot = x.tblSlot.slot,
                        bookedSlotID = x.bookedSlotID,
                        hallID = x.tblBookedHall.hallID
                    }).ToList();
                    bookingSlotList.AddRange(bookedSlots);
                }
                allSlotsList = bookingSlotList;
            }
            return bookingSlotList;
        }
        #endregion
    }
}