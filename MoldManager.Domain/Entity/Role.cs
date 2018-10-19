/*
 * Create By:lechun1
 * 
 * Description:data of role record
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Role
    {
        public int RoleID { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public bool ProjectBased { get; set; }
    }
}
