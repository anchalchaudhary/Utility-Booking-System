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
    
    public partial class tblHall
    {
        public tblHall()
        {
            this.tblBookedHalls = new HashSet<tblBookedHall>();
            this.tblRequirementForHalls = new HashSet<tblRequirementForHall>();
        }
    
        public int hallID { get; set; }
        public string hallName { get; set; }
    
        public virtual ICollection<tblBookedHall> tblBookedHalls { get; set; }
        public virtual ICollection<tblRequirementForHall> tblRequirementForHalls { get; set; }
    }
}
