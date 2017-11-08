using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Models;

namespace UtilityBookingSystem.Repository
{
    public class UserPanelRepository
    {
        public bool UserLogin(Users objUser)
        {
            bool isLoggedIn = false;
            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                tblUser objtblUser = db.tblUsers.SingleOrDefault(x => x.email == objUser.email && x.password == objUser.password);
                if (objtblUser != null)
                {
                    isLoggedIn = true;
                }
            }
            return isLoggedIn;
        }
    }
}