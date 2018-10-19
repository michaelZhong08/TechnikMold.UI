/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class ProjectRole
    {
        public int ProjectRoleID { get; set; }
        public int ProjectID { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }

        public ProjectRole()
        {
            ProjectRoleID = 0;
            ProjectID = 0;
            RoleID = 0;
            UserID = 0;
            UserName = "";
        }
    }
}
