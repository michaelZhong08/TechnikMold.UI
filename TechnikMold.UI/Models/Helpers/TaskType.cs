using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoldManager.WebUI.Models.Helpers
{
    public class TaskType
    {
        private Dictionary<int, string> Types;

        public TaskType()
        {
            Types = new Dictionary<int, string>();
            Types.Add(1, "电极任务");           
            Types.Add(2, "EDM任务");
            Types.Add(3, "WEDM任务");
            Types.Add(4, "铣铁任务");
            Types.Add(5, "QC任务");
            Types.Add(6, "铣磨任务");
        }

        public string GetTypeName(int TypeID)
        {
            return Types[TypeID];
        }
    }
}