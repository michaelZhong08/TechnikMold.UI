using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class ElectrodeGridRowModel
    {
        public string[] cell;

        public ElectrodeGridRowModel(CNCItem CNCItem, string TaskName, DateTime FinishDate)
        {
            cell = new string[7];
            cell[0] = CNCItem.CNCItemID.ToString();
            cell[1] = CNCItem.LabelName;
            cell[2] = CNCItem.GapCompensation.ToString();
            cell[3] = CNCItem.ZCompensation.ToString();
            cell[4] = TaskName;
            if (FinishDate == new DateTime(1900, 1, 1))
            {
                cell[5] = "-";
            }
            else
            {
                cell[5] = FinishDate.ToString("yyyy-MM-dd HH:mm");
            }
            cell[6] = CNCItem.Gap.ToString();
        }
    }
}