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
            cell[2] = Item.Specification;
            cell[3] = Item.Quantity.ToString();
            cell[4] = Item.UnitPriceWT.ToString();
            cell[5] = Item.TotalPriceWT.ToString();
            //bool _date = Item.PlanTime.ToString("yyyy-MM-dd");
            if (Item.SupplierID > 0)
            {
                cell[6] = Item.PlanTime.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : Item.PlanTime.ToString("yyyy-MM-dd");
            }
            else
            {
                cell[6] = Item.RequireTime.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : Item.RequireTime.ToString("yyyy-MM-dd");
            }
            cell[7] = Item.Memo;
            
        }
    }
}