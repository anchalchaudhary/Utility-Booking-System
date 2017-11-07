using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class Users
    {
        public int userID { get; set; }
        [Required(ErrorMessage="Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Select a Department")]
        public Nullable<int> departmentID { get; set; }
    }
}