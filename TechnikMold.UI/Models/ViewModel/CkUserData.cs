using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikMold.UI.Models.ViewModel
{
    public class CkUserData
    {
        public int UserID { get; set; }
        public string UserCode { get; set; }
        public string LogonName { get; set; }
        public string FullName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int PositionID { get; set; }
        public string PositionName { get; set; }
    }
}