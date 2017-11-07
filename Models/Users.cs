using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Repository;
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

        AdminPanelRepository objAdminPanelRepository = new AdminPanelRepository();
        public bool AddNewUser(Users model) //Adds new User
        {
            bool isAdded = objAdminPanelRepository.SaveUserDetails(model);
            return isAdded;
        }
    }
}