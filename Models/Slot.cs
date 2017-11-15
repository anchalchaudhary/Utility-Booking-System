using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class Slot
    {
        #region tblSlot attributes
        public int slotID { get; set; }
        public string slot { get; set; }
        #endregion

        #region Get All Slots
        public List<tblSlot> GetAllSlotsList()
        {
            List<tblSlot> allSlotsList;
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                // context.Configuration.ProxyCreationEnabled = true;

                allSlotsList = context.tblSlots.ToList();
            }
            return allSlotsList;
        }
        #endregion
    }
}