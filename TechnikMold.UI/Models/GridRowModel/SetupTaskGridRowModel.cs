using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikMold.UI.Models.ViewModel;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Status;

namespace TechnikMold.UI.Models.GridRowModel
{
    public class SetupTaskGridRowModel
    {
        public string[] cell;
        public SetupTaskGridRowModel(SetupTaskStart _setupTask)
        {
            cell = new string[13];
            cell[0] = _setupTask.TaskID.ToString();
            cell[1] = _setupTask.TaskName.ToString();
            cell[2] = _setupTask.State; //Enum.GetName(typeof(CNCStatus), _task.State) ?? "-";
            cell[3] = _setupTask.MachinesCode;
            cell[4] = _setupTask.MachinesName;
            cell[5] = _setupTask.UserName;
            cell[6] = _setupTask.UserID.ToString();
            cell[7] = _setupTask.Qty.ToString();
            cell[8] = _setupTask.TotalTime.ToString();

            cell[9] = _setupTask.StartTime.ToString("yyyy/MM/dd HH:mm:ss");
            cell[10] = _setupTask.FinishTime.ToString("yyyy/MM/dd HH:mm:ss");
            cell[11] = _setupTask.SemiTaskFlag;
            cell[12] = _setupTask.TaskHourID.ToString();
        }
    }
}