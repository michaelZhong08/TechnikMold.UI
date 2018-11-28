using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikMold.UI.Models.GridRowModel
{
    public class PartSearchGridRowModel
    {
        public string[] cell;
        public PartSearchGridRowModel(PurchaseItem _item)
        {
            cell = new string[8];
            cell[0] = _item.PartID.ToString();
            cell[1] = _item.Name;
            cell[2] = _item.PartNumber;
            cell[3] = _item.Specification ?? "";
            cell[4] = _item.Material ?? "";
            cell[5] = _item.SupplierName ?? "";
            cell[6] = _item.Quantity.ToString();
            cell[7] = "";
        }
    }
}