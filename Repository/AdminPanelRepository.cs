using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Models;
namespace UtilityBookingSystem.Repository
{
    public class AdminPanelRepository
    {
        #region Save New User Details
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
                    objtblUser.contact = model.contact;
                    objtblUser.password = model.password;
                    objtblUser.deptID = model.deptID;
                    db.tblUsers.Add(objtblUser);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Gets Registered Users List
        public List<Users> GetRegisteredUsers()
        {
            List<Users> registeredUsersList;
            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                registeredUsersList = db.tblUsers.Select(x => new Users
                {
                    userID = x.userID,
                    name = x.name,
                    email = x.email,
                    contact = x.contact,
                    dept = x.tblDepartment.department
                }).ToList();
            }
            return registeredUsersList;
        }
        #endregion

        public bool DeleteUser(int id)
        {
            try
            {
                tblUser objtblUser = new tblUser();
                using (BookingSystemDBEntities db = new BookingSystemDBEntities())
                {
                    objtblUser = db.tblUsers.First(x => x.userID == id);
                }
                using (BookingSystemDBEntities db = new BookingSystemDBEntities())
                {
                    db.tblUsers.Attach(objtblUser);
                    db.tblUsers.Remove(objtblUser);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}