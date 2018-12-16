using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class PurchaseOrderItemGridRowModel
    {
        public string[] cell;

        public PurchaseOrderItemGridRowModel(PurchaseItem Item){
            cell = new string[10];
            cell[0] = Item.PurchaseItemID.ToString();
            cell[1] = Item.Name;
            cell[2] = Item.PartNumber;
            cell[3] = Item.Specification;
            cell[4] = Item.Quantity.ToString();
            cell[5] = Item.UnitPriceWT.ToString();
            cell[6] = Item.TotalPriceWT.ToString();
            //bool _date = Item.PlanTime.ToString("yyyy-MM-dd");
            if (Item.SupplierID > 0)
            {
                cell[7] = Item.PlanTime.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : Item.PlanTime.ToString("yyyy-MM-dd");
            }
            else
            {
                cell[7] = Item.RequireTime.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : Item.RequireTime.ToString("yyyy-MM-dd");
            }
            cell[8] = Item.Memo;
            
        }
    }
}