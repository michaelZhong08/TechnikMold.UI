using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Status;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class CNCItemGridRowModel
    {
        public string[] cell;
        public CNCItemGridRowModel(Task _task,CNCItem Item)
        {
            cell = new string[11];
            cell[0] = Item.CNCItemID.ToString();
            cell[1] = Item.LabelName;
            cell[2] = Item.Ready.ToString() ;
            cell[3] = Item.Required.ToString();
            cell[4] = Item.Material;
            cell[5] = Item.SafetyHeight.ToString();
            cell[6] = _task.Raw;
            cell[7] = Item.LabelPrinted.ToString();
            cell[8] = Item.Status >= (int)CNCItemStatus.CNC结束? true.ToString() : false.ToString();

            cell[9] = Item.CNCStartTime.ToString("yyyy-MM-dd HH:mm");
            cell[10] = Item.CNCFinishTime.ToString("yyyy-MM-dd HH:mm");
        }
    }
}