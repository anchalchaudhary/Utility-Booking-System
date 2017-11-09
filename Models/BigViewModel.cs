using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class BigViewModel
    {
        public Departments departments { get; set; }
        public Users users { get; set; }
        public Login login { get; set; }
        public Booking booking { get; set; }
        public BookedDate bookedDate { get; set; }
        public BookedHall bookedHall { get; set; }
        public BookedRequirement bookedRequirement { get; set; }
        public BookedSlot bookedSlot { get; set; }
        public Hall hall { get; set; }
        public Requirement requirement { get; set; }
        public Slot slot { get; set; }
    }
}