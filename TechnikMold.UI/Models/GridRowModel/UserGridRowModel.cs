using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class UserGridRowModel
    {

        public string[] cell;
        public UserGridRowModel(User User, string DepartmentName, string PositionName)
        {
            cell = new string[11];
            
            cell[0] = User.UserID.ToString();
            cell[1] = User.DepartmentID.ToString();
            cell[2] = User.UserCode?? "";
            cell[3] = User.LogonName;
            cell[4] = User.FullName;
            cell[5] = DepartmentName;
            cell[6] = PositionName;
            cell[7] = User.Extension;
            cell[8] = User.Mobile;
            cell[9] = User.Email;                
            cell[10] = User.Enabled.ToString();
            

        }
    }
}