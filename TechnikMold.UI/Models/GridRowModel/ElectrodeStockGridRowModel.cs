using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Status;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class ElectrodeStockGridRowModel
    {
        public string[] cell;

        public ElectrodeStockGridRowModel(CNCItem CNCItem)
        {
            cell = new string[5];
            cell[0] = CNCItem.CNCItemID.ToString();
            cell[1] = CNCItem.LabelName;
            cell[2] = CNCItem.Material;
            cell[3] = CNCItem.CreateTime.ToString("yyyy-MM-dd HH:mm");
            cell[4] = Enum.GetName(typeof(TaskStatus), CNCItem.Status);
        }
    }
}