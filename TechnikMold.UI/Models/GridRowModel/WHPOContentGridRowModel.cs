using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;


namespace MoldManager.WebUI.Models.GridRowModel
{
    public class WHPOContentGridRowModel
    {

        public string[] cell;


        public WHPOContentGridRowModel(PurchaseItem PurchaseItem)
        {
            cell = new string[10];

            cell[0] = PurchaseItem.PurchaseItemID.ToString();
            cell[1] = PurchaseItem.Name;
            cell[2] = PurchaseItem.PartNumber;
            cell[3] = PurchaseItem.Specification;
            cell[4] = PurchaseItem.Quantity.ToString();
            cell[5] = PurchaseItem.InStockQty.ToString();
            cell[6] = PurchaseItem.PlanTime == new DateTime(1900, 1, 1) ? "" : PurchaseItem.PlanTime.ToString("yyyy-MM-dd");
            cell[7] = PurchaseItem.Memo;

        }
    }
}