using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoldManager.WebUI.Models.EditModel
{
    public class UserRoleEditModel
    {
        public int UserRoleID { get; set; }
        public string DisplayName { get; set; }

        public UserRoleEditModel(int ID, string Display)
        {
            UserRoleID = ID;
            DisplayName = Display;

        }

    }
}