using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class WHRequestItemGridRowModel
    {
        public string[] cell;

        public WHRequestItemGridRowModel(WarehouseRequestItem Item,WHStock _stockItem)
        {
            cell = new string[14];
            cell[0] = Item.WarehouseRequestItemID.ToString();
            cell[1] = Item.PartName;
            cell[2] = Item.PartNumber;
            cell[3] = Item.Specification;
            cell[4] = _stockItem.Qty.ToString();
            cell[5] = Item.Quantity.ToString();
            cell[6] = Item.ReceivedQuantity.ToString();
            cell[7] = Item.ShortQty.ToString();

            cell[8] = "";
            cell[9] = "";
            cell[10] = "";
            cell[11] = "";
            cell[12] = Item.PartID.ToString();
            cell[13] = _stockItem.ID.ToString();
        }
    }
}