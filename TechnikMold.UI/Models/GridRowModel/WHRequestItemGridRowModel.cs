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

        public WHRequestItemGridRowModel(WarehouseRequestItem Item)
        {
            cell = new string[10];
            cell[0] = Item.WarehouseRequestItemID.ToString();
            cell[1] = Item.PartName;
            cell[2] = Item.PartNumber;
            cell[3] = Item.Specification;
            cell[4] = Item.Quantity.ToString();
            cell[5] = Item.Quantity.ToString();
            cell[6] = Item.ReceivedQuantity.ToString();
        }
    }
}