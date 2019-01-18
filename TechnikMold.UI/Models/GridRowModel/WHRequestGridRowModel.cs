using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikMold.UI.Models;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Status;


namespace MoldManager.WebUI.Models.GridRowModel
{
    public class WHRequestGridRowModel
    {
        public string[] cell;

        public WHRequestGridRowModel(WarehouseRequest Request, string UserName,string ApprovalUser)
        {
            cell = new string[7];
            cell[0] = Request.WarehouseRequestID.ToString();
            cell[1] = Request.RequestNumber;
            cell[2] = UserName;            
            cell[3] = Request.CreateDate.ToString("yyyy-MM-dd HH:mm");
            cell[4] = ApprovalUser;
            cell[5] = Toolkits.CheckZero(Request.ApprovalDate)?"-": Request.ApprovalDate.ToString("yyyy-MM-dd HH:mm");
            cell[6] = Enum.GetName(typeof(WarehouseRequestStatus), Request.State);
        }
    }
}