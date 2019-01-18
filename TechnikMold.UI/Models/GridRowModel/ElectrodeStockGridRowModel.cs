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
            cell = new string[6];
            cell[0] = CNCItem.CNCItemID.ToString();
            cell[1] = CNCItem.LabelName;
            cell[2] = CNCItem.Material;
            cell[3] = CNCItem.CreateTime.ToString("yyyy-MM-dd HH:mm");
            cell[4] = Enum.GetName(typeof(CNCItemStatus), CNCItem.Status);
            string middle = "0000000000" + CNCItem.ELE_INDEX.ToString();
            try
            {
                middle = middle.Substring(middle.Length - 10, 10) ?? "";
            }
            catch
            {
                middle = "";
            }
            middle = "*EI" + middle + "*";
            cell[5] = middle;
        }
    }
}