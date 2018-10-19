/*
 * Create By:lechun1
 * 
 * Description:data represent user information
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class User
    {
        public int UserID { get; set; }
        public string UserCode { get; set; }
        public string LogonName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Extension { get; set; }
        public string Mobile { get; set; }
        public int DepartmentID { get; set; }
        public bool Enabled { get; set; }
        public string EmailUser { get; set; }
        public string EmailPassword { get; set; }
        public int PositionID { get; set; }
    }
}
