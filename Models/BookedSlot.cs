using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class BookedSlot
    {
        public int bookedSlotID { get; set; }
        public Nullable<int> bookedHallID { get; set; }
        public Nullable<int> slotID { get; set; }
    }
}