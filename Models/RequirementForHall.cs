using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class RequirementForHall
    {
        public int reqHallID { get; set; }
        public Nullable<int> hallID { get; set; }
        public Nullable<int> requirementID { get; set; }
        public string requirementName { get; set; }
        public List<RequirementForHall> GetRequirementsList(int hallID)
        {
            List<RequirementForHall> requirementsList;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                // context.Configuration.ProxyCreationEnabled = true;

                requirementsList = context.tblRequirementForHalls.Where(x => x.hallID == hallID).Select(x => new RequirementForHall
                {
                    reqHallID = x.reqHallID,
                    hallID = x.hallID,
                    requirementID = x.tblRequirement.requirementID,
                    requirementName = x.tblRequirement.requirementName
                }).ToList();
            }
            return requirementsList;
        }
    }
}