using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Repository;

namespace UtilityBookingSystem.Models
{
    public class Hall
    {
        #region tblHall attributes
        public int hallID { get; set; }
        public string hallName { get; set; }
        public Nullable<int> requirementID { get; set; }
        #endregion
        #region Repository objects
        BookingRepository objBookingRepository = new BookingRepository();
        #endregion
        #region Get List of Halls
        public List<tblHall> GetHallsList()
        {
            List<tblHall> hallsList;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
               // context.Configuration.ProxyCreationEnabled = true;

                hallsList =context.tblHalls.ToList();
            }

            return hallsList;
        }
        #endregion
    }
}