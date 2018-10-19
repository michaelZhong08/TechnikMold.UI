using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikMold.UI.Models.GridRowModel
{
    public class MGUptVerGridRowModel
    {
        public string[] cell;
        public MGUptVerGridRowModel(Task task,string DrawingFile, string CAM="")
        {
            cell = new string[6];
            cell[0] = task.TaskID.ToString();
            cell[1] = task.TaskName ?? "";
            cell[2] = CAM;
            cell[3] = task.ProcessName;
            cell[4] = task.Quantity.ToString();
            cell[5] = DrawingFile;
        }
    }
}