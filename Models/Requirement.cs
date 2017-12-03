using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class Requirement
    {
        public int requirementID { get; set; }
        public string requirementName { get; set; }
        public int hallID { get; set; }
        public bool isSelected { get; set; }
        public List<Requirement> GetRequirementsList(int hallID)
        {
            List<Requirement> requirementsList;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                // context.Configuration.ProxyCreationEnabled = true;

                requirementsList = context.tblRequirementForHalls.Where(x=>x.hallID==hallID).Select(x=> new Requirement{
                    requirementID = x.tblRequirement.requirementID,
                    requirementName = x.tblRequirement.requirementName
                }).ToList();
            }
            return requirementsList;
        }
    }
}