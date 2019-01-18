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
            cell[1] = TaskName;
            cell[2] = CNCItem.LabelName;
            cell[3] = CNCItem.Gap.ToString();
            cell[4] = CNCItem.GapCompensation.ToString();
            cell[5] = CNCItem.ZCompensation.ToString();
            
            if (FinishDate == new DateTime(1900, 1, 1))
            {
                cell[6] = "-";
            }
            else
            {
                cell[6] = FinishDate.ToString("yyyy-MM-dd HH:mm");
            }
            
        }
    }
}