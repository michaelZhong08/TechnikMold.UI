using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoldManager.WebUI.Models.Helpers
{
    public class TaskResponsibleType
    {
        private Dictionary<int, string> Types;

        public TaskResponsibleType()
        {
            Types = new Dictionary<int, string>();
            
            Types.Add(1, "CAD");
            Types.Add(2, "CAM");
            Types.Add(3, "加工");
            Types.Add(4, "质检");
        }

        public string GetTypeName(int TypeID)
        {
            return Types[TypeID];
        }
    }
}