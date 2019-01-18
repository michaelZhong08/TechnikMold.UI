using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class TaskFinishGridRowModel
    {
        public string[] cell;
        public TaskFinishGridRowModel(Task Task)
        {
            
            cell = new string[4];

            cell[0] = Task.TaskID.ToString();
            cell[1] = Task.TaskName;
            cell[2] = Task.StartTime.ToString("yyyy-MM-dd HH:mm");
            cell[3] = "";

        }

        public TaskFinishGridRowModel(CNCItem Item,string machineName)
        {
            cell = new string[4];
            cell[0] = Item.CNCItemID.ToString();
            cell[1] = Item.LabelName;
            cell[2] = Item.CNCStartTime.ToString("yyyy-MM-dd HH:mm");
            cell[3] = machineName;//Item.CNCMachine;
        }
    }
}