using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoldManager.WebUI.Models.EditModel
{
    public class UserRoleEditModel
    {
        public int UserRoleID { get; set; }
        public int DepID { get; set; }
        public int Position { get; set; }
        public string DisplayName { get; set; }

        public UserRoleEditModel(int userRoleID,int depID,int position,string Display)
        {
            UserRoleID = userRoleID;
            DepID = depID;
            Position = position;
            DisplayName = Display;
        }
    }
}