//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UtilityBookingSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblBookedSlot
    {
        public int bookedSlotID { get; set; }
        public Nullable<int> bookedHallID { get; set; }
        public Nullable<int> slotID { get; set; }
    
        public virtual tblBookedHall tblBookedHall { get; set; }
        public virtual tblSlot tblSlot { get; set; }
    }
}
