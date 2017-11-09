using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UtilityBookingSystem.Models
{
    public class Departments
    {
        #region tblDepartment attributes
        public int deptID { get; set; }
        public string department { get; set; }
        #endregion

        #region Gets Departments List
        public List<tblDepartment> GetDepartmentsList()
        {
            List<tblDepartment> deptList;
            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                deptList = db.tblDepartments.ToList();
            }
            return deptList;
        }
        #endregion
    }
}