using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class PurchaseOrderReportGridRowModel
    {
        public string[] cell;

        public PurchaseOrderReportGridRowModel(PurchaseItem Item, 
            string MainType, 
            string SubType, 
            string SupplierName, 
            string CostCenter, 
            string PurchaseOrder)
        {
            cell = new string[15];
            cell[0] = Item.PurchaseItemID.ToString();
            cell[1] = Item.DeliveryTime.ToString("yyyy-MM");
            cell[2] = CostCenter;
            switch (Item.ProcedureType)
            {
                case 0:
                    cell[3] = "";
                    break;
                case 10:
                    cell[3] = "制模";
                    break;
                case 20:
                    cell[3] = "修模";
                    break;
                case 30:
                    cell[3] = "耗材";
                    break;
            }
            cell[4] = MainType;
            cell[5] = SubType;
            cell[6] = SupplierName;
            cell[7] = Item.PartNumber;
            cell[8] = Item.Specification;
            cell[9] = Item.Quantity.ToString();
            cell[10] = Item.UnitPrice.ToString();
            cell[11] = Item.TotalPrice.ToString();
            cell[12] = Item.TaxRate.ToString() + "%";
            cell[13] = PurchaseOrder;
        }
    }
}