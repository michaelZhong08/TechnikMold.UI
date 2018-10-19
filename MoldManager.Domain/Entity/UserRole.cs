using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class UserRole
    {

        //[Key]
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
        public int PositionID { get; set; }
        public bool DefaultRole { get; set; }
        public bool Enabled { get; set; }

        public UserRole()
        {
            UserRoleID = 0;
            UserID = 0;
            DepartmentID = 0;
            PositionID = 0;
            DefaultRole = false;
            Enabled = true;
        }

        public UserRole(int _UserID, int _DepartmentID, int _PositionID)
        {
            UserRoleID = 0;
            UserID = _UserID;
            DepartmentID = _DepartmentID;
            PositionID = _PositionID;
            DefaultRole = false;
            Enabled = true;
        }
    }
}
