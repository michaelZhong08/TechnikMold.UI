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
            cell = new string[10];

            cell[0] = User.UserID.ToString();
            cell[1] = User.DepartmentID.ToString();
            cell[2] = User.LogonName;
            cell[3] = User.FullName;
            cell[4] = DepartmentName;
            cell[5] = PositionName;
            cell[6] = User.Extension;
            cell[7] = User.Mobile;
            cell[8] = User.Email;                
            cell[9] = User.Enabled.ToString();
            

        }
    }
}