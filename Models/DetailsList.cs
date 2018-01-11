using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class DetailsList
    {
        [Required(ErrorMessage = "Date is required.")]
        public DateTime date { get; set; }
        [Required(ErrorMessage = "Select a hall and corresponding details.")]
        public List<Hall> hallsArray { get; set; }
        public Nullable<bool> nonWorkingHours { get; set; }

    }
}