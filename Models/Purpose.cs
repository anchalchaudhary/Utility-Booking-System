using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class Purpose
    {
        #region tblPurpose attributes
        public int purposeID { get; set; }
        public string purpose { get; set; }
        #endregion

        #region Gets Purpose List
        public List<tblPurpose> GetPurposeList()
        {
            List<tblPurpose> purposeList;
            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                purposeList = db.tblPurposes.ToList();
            }
            return purposeList;
        }
        #endregion
    }
}