using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;


namespace MoldManager.WebUI.Models.GridRowModel
{
    public class WHPOListGridRowModel
    {
        public string[] cell;

        public WHPOListGridRowModel(PurchaseOrder PurchaseOrder, string UserName, string StatusName,string SupplierName)
        {
            cell = new string[6];
            cell[0] = PurchaseOrder.PurchaseOrderID.ToString();
            cell[1] = PurchaseOrder.PurchaseOrderNumber;
            cell[2] = SupplierName;
            cell[3] = UserName;
            cell[4] = PurchaseOrder.ReleaseDate.ToString("yyyy-MM-dd");
            cell[5] = StatusName;
        }
    }
}