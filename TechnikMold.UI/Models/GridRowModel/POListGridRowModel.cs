using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;


namespace MoldManager.WebUI.Models.GridRowModel
{
    public class POListGridRowModel
    {
        public string[] cell;

        public POListGridRowModel(PurchaseOrder PurchaseOrder, 
            string SupplierName, 
            string StatusName, 
            string PurchaseType, 
            string UserName)
        {
            cell = new string[10];
            cell[0] = PurchaseOrder.PurchaseOrderID.ToString();
            cell[1] = PurchaseOrder.PurchaseOrderNumber;
            cell[2] = SupplierName;
            cell[3] = PurchaseOrder.TotalPrice.ToString();
            cell[4] = PurchaseOrder.DueDate.ToString("yyyy-MM-dd");
            cell[5] = StatusName;
            cell[6] = PurchaseOrder.Memo;
            cell[7] = PurchaseType;
            cell[8] = UserName;
            cell[9] = PurchaseOrder.CreateDate == new DateTime(1900, 1, 1) ? "" : PurchaseOrder.CreateDate.ToString("yyyy-MM-dd");
        }
    }
}