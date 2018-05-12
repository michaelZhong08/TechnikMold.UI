using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Status;


namespace MoldManager.WebUI.Models.GridRowModel
{
    public class WHRequestGridRowModel
    {
        public string[] cell;

        public WHRequestGridRowModel(WarehouseRequest Request, string UserName)
        {
            cell = new string[5];
            cell[0] = Request.WarehouseRequestID.ToString();
            cell[1] = Request.RequestNumber;
            cell[2] = UserName;
            cell[3] = Enum.GetName(typeof(WarehouseRequestStatus), Request.State);
            cell[4] = Request.CreateDate.ToString("yyyy-MM-dd HH:mm");
        }
    }
}