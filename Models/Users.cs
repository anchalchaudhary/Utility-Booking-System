using System;
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
        public int userID { get; set; }
        [Required(ErrorMessage="Name is required")]
        //[RegularExpression(@"[A-Z]'?[- a-zA-Z]( [a-zA-Z])*", ErrorMessage ="Invalid Name")]
        public string name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w)+)+)", ErrorMessage = "Email is not valid")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Select a Department")]
        public Nullable<int> deptID { get; set; }

        [RegularExpression(@"([6-9][0-9]{9})",ErrorMessage = "Invalid phone number" )]
        [Required(ErrorMessage = "Enter Contact Number")]
        public string contact { get; set; }

        public string dept { get; set; }

        AdminPanelRepository objAdminPanelRepository = new AdminPanelRepository();
        UserPanelRepository objUserPanelRepository = new UserPanelRepository();
        public bool AddNewUser(BigViewModel model) //Adds new User
        {
            Users objUsers = new Users();
            objUsers.name = model.users.name;
            objUsers.email = model.users.email;
            objUsers.deptID = model.users.deptID;
            objUsers.password = Encryption.Encrypt(model.users.password);
            objUsers.contact = model.users.contact;
            bool isAdded = objAdminPanelRepository.SaveUserDetails(objUsers);
            return isAdded;
        }
        public List<Users> GetRegisteredUsers()
        {
            List<Users> registeredUsersList = objAdminPanelRepository.GetRegisteredUsers();
            return registeredUsersList;
        }
        public bool UserLogin(BigViewModel model)
        {
            Users objUsers = new Users();
            objUsers.email = model.login.loginID;
            objUsers.password = Encryption.Encrypt(model.login.password);
            bool isLoggedIn = objUserPanelRepository.UserLogin(objUsers);

            return isLoggedIn;
        }
    }
}