using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class BookedDate
    {
        public int dateID { get; set; }
        public Nullable<System.DateTime> dateChosen { get; set; }
        public Nullable<int> bookingID { get; set; }
    }
}