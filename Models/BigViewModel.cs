using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class BigViewModel
    {
        public Departments departments { get; set; }
        public Users users { get; set; }
        public Login login { get; set; }
    }
}