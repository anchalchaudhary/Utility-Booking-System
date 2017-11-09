using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class Hall
    {
        public int hallID { get; set; }
        public string hallName { get; set; }
        public Nullable<int> requirementID { get; set; }
    }
}