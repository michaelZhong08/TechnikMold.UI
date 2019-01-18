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


        public WHPOContentGridRowModel(PurchaseItem PurchaseItem,string warehoseListStr,string whpositionListStr,WHStock _whstock)
        {
            cell = new string[12];

            cell[0] = PurchaseItem.PurchaseItemID.ToString();
            cell[1] = PurchaseItem.Name;
            cell[2] = PurchaseItem.PartNumber;
            cell[3] = PurchaseItem.Specification;
            cell[4] = PurchaseItem.Quantity.ToString();
            cell[5] = PurchaseItem.InStockQty.ToString();

            cell[6] = "0";
            cell[7] = warehoseListStr;//"<select class='form-control'><option>wh1</option></select>";
            cell[8] = whpositionListStr;
            cell[9] = PurchaseItem.PlanTime == new DateTime(1900, 1, 1) ? "" : PurchaseItem.PlanTime.ToString("yyyy-MM-dd");
            cell[10] = _whstock.Memo;
            cell[11] = _whstock.ID.ToString();
        }
    }
}