using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Models;
namespace UtilityBookingSystem.Repository
{
    public class AdminPanelRepository
    {
        public List<tblDepartment> getDepartmentsList()
        {
            List<tblDepartment> deptList;
            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                deptList = db.tblDepartments.ToList();
            }
            return deptList;
        }
        public bool SaveUserDetails(Users model)    //Save User details
        {
            bool checkEmailCount;
            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                checkEmailCount = db.tblUsers.Any(m => m.email == model.email); //checking if the email already exists for any user
            }
            if (checkEmailCount == false)
            {
                using (BookingSystemDBEntities db = new BookingSystemDBEntities()) // Save details to DB
                {
                    tblUser objtblUser = new tblUser();
                    objtblUser.name = model.name;
                    objtblUser.email = model.email;
                    objtblUser.password = model.password;
                    objtblUser.deptID = model.deptID;
                    db.tblUsers.Add(objtblUser);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}