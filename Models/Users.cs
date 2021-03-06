﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Repository;
using UtilityBookingSystem.Extra_Classes;
namespace UtilityBookingSystem.Models
{
    public class Users
    {
        #region tblUser attributes
        public int userID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        //[RegularExpression(@"[A-Z]'?[- a-zA-Z]( [a-zA-Z])*", ErrorMessage ="Invalid Name")]
        public string name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w)+)+)", ErrorMessage = "Email is not valid")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Select a Department")]
        public Nullable<int> deptID { get; set; }

        [RegularExpression(@"([6-9][0-9]{9})", ErrorMessage = "Invalid phone number")]
        [Required(ErrorMessage = "Enter Contact Number")]
        public string contact { get; set; }

        public string dept { get; set; }
        #endregion
        [Required(ErrorMessage = "Re-Enter Password")]
        [Compare("password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        #region Repository objects
        AdminPanelRepository objAdminPanelRepository = new AdminPanelRepository();
        UserPanelRepository objUserPanelRepository = new UserPanelRepository();
        #endregion

        #region Add new User
        public bool AddNewUser(BigViewModel model)
        {
            Users objUsers = new Users();
            objUsers.name = model.users.name;
            objUsers.email = model.users.email;
            objUsers.deptID = model.users.deptID;
            objUsers.password = Encryption.Encrypt(model.users.password); //Encrypts the password
            objUsers.contact = model.users.contact;

            bool isAdded = objAdminPanelRepository.SaveUserDetails(objUsers); //Adds details of New user to DB (AdminPanel Repository)

            return isAdded;
        }
        #endregion

        #region List of Registered Users
        public List<Users> GetRegisteredUsers()
        {
            List<Users> registeredUsersList = objAdminPanelRepository.GetRegisteredUsers(); //Gets List of Registered Users from AdminPanel Repository
            return registeredUsersList;
        }
        #endregion

        #region User Login
        public int UserLogin(BigViewModel model)
        {
            Users objUsers = new Users();
            objUsers.dept = model.login.loginID.ToUpper();
            objUsers.password = Encryption.Encrypt(model.login.password); //encrypts the password

            int userID = objUserPanelRepository.UserLogin(objUsers); //Verifies User's login details from UserPanel Repository and fetches his userID

            return userID;
        }
        #endregion

        #region User Details
        public Users GetUserDetails(int userID)
        {
            Users userDetail = new Users();
            tblUser objtblUser = new tblUser();
            tblDepartment objtblDepartment = new tblDepartment();
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                objtblUser = context.tblUsers.First(x => x.userID == userID);
            }
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                objtblDepartment = context.tblDepartments.First(x => x.deptID == objtblUser.deptID);
            }
            userDetail.userID = objtblUser.userID;
            userDetail.name = objtblUser.name;
            userDetail.email = objtblUser.email;
            userDetail.contact = objtblUser.contact;
            userDetail.dept = objtblDepartment.department;
            return userDetail;
        }
        #endregion

        public int getUserID(string email, int deptID)
        {
            int userID;
            Users userDetail = new Users();
            tblUser objtblUser = new tblUser();
            using (var context = new BookingSystemDBEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                objtblUser = context.tblUsers.First(x => x.email == email && x.deptID == deptID);
            }
            if (objtblUser != null)
                userID = objtblUser.userID;
            else
                userID = 0;
            return userID;
        }
        public bool DeleteUser(int id)
        {
            bool isDeleted = objAdminPanelRepository.DeleteUser(id);
            return isDeleted;
        }
    }
}