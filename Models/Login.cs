using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class Login
    {
        [Required(ErrorMessage="Login ID is required.")]
        public string loginID { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string password { get; set; }
    }
}