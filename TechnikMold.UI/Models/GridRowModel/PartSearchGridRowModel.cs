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
        public PartSearchGridRowModel(Part _item)
        {
            cell = new string[14];
            cell[0] = _item.PartID.ToString();
            cell[1] = _item.ShortName;
            cell[2] = _item.PartNumber;
            cell[3] = _item.Specification ?? "";
            cell[4] = _item.MaterialName ?? "";
            cell[5] = _item.SupplierName ?? "";
            cell[6] = _item.Quantity.ToString();
            cell[7] = _item.Version;

            cell[8] = _item.Hardness;
            cell[9] = _item.JobNo;
            cell[10] = _item.MoldNumber;
            cell[11] = _item.ERPPartID;
            cell[12] = _item.PlanQty.ToString();
            cell[13] = _item.Memo;
        }
    }
}