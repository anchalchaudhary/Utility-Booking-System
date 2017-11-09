using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class BookedRequirement
    {
        public int bookedReqID { get; set; }
        public Nullable<int> bookingID { get; set; }
        public Nullable<int> hallID { get; set; }
    }
}