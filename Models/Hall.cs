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

        public List<Requirement> requirementsArray { get; set; }
        public List<Slot> slotsArray { get; set; }
        public int noOfChairs { get; set; }
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

        public string otherRequirements { get; set; }


        public List<Hall> GetHallDetails()
        {
            List<Hall> hallsList = new List<Hall>();
            List<tblHall> listtblHall = new List<tblHall>();
            List<tblSlot> listtblSlot = new List<tblSlot>();
            List<tblRequirement> listtblRequirement = new List<tblRequirement>();
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                listtblHall = context.tblHalls.ToList();
                listtblSlot = context.tblSlots.ToList();
                listtblRequirement = context.tblRequirements.ToList();
            }
            foreach (var item in listtblHall)
            {
                hallsList.Add(new Hall()
                {
                    hallID=item.hallID,
                    hallName = item.hallName,
                });
            }
            foreach (var item in hallsList)
            {
                item.slotsArray = new List<Slot>();
                foreach (var x in listtblSlot)
                {
                    item.slotsArray.Add(new Slot()
                    {
                        slotID = x.slotID,
                        slot = x.slot
                    });
                }
            }
            foreach (var item in hallsList)
            {
                item.requirementsArray = new List<Requirement>();
                foreach (var x in listtblRequirement)
                {
                    item.requirementsArray.Add(new Requirement()
                    {
                        requirementID = x.requirementID,
                        requirementName = x.requirementName
                    });
                }
            }
            return hallsList;
        }


    }
}