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
    
    public partial class tblBookedRequirement
    {
        public int bookedReqID { get; set; }
        public Nullable<int> bookingID { get; set; }
        public Nullable<int> hallID { get; set; }
        public Nullable<int> requirementID { get; set; }
    
        public virtual tblBooking tblBooking { get; set; }
        public virtual tblHall tblHall { get; set; }
        public virtual tblRequirement tblRequirement { get; set; }
    }
}