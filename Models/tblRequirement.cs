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
    
    public partial class tblRequirement
    {
        public tblRequirement()
        {
            this.tblRequirementForHalls = new HashSet<tblRequirementForHall>();
            this.tblBookedRequirements = new HashSet<tblBookedRequirement>();
        }
    
        public int requirementID { get; set; }
        public string requirementName { get; set; }
    
        public virtual ICollection<tblRequirementForHall> tblRequirementForHalls { get; set; }
        public virtual ICollection<tblBookedRequirement> tblBookedRequirements { get; set; }
    }
}
