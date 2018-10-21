using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Status;

namespace TechnikMold.UI.Models.GridRowModel
{
    public class SetupTaskGridRowModel
    {
        public string[] cell;
        public SetupTaskGridRowModel(Task _task)
        {
            cell = new string[8];
            cell[0] = _task.TaskID.ToString();
            cell[1] = _task.TaskName.ToString();
            cell[2] = Enum.GetName(typeof(CNCStatus), _task.State) ?? "-";
            cell[3] = "";
            cell[4] = "";
            cell[5] = "";
            cell[6] = "";
            cell[7] = "";
        }
    }
}