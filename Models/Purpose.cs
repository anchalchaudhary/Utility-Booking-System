using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class Purpose
    {
        public int purposeID { get; set; }
        public string purpose { get; set; }

        public List<tblPurpose> GetPurposeList()
        {
            List<tblPurpose> purposeList;
            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                purposeList = db.tblPurposes.ToList();
            }
            return purposeList;
        }
    }
}