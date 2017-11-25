using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class DetailsList
    {
        public DateTime date { get; set; }
        public List<Hall> hallsArray { get; set; }
        
    }
}