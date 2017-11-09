using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class BookedHall
    {
        public int bookedHallID { get; set; }
        public Nullable<int> hallID { get; set; }
        public Nullable<int> dateID { get; set; }
    }
}