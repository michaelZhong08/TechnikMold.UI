using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikMold.UI.Models.GridRowModel
{
    public class PurchaseAttachGridRowModel
    {
        public string[] cell;
        public PurchaseAttachGridRowModel(PurchaseItem _item,AttachFileInfo model)
        {
            cell = new string[13];
            cell[0] = model.ObjID ?? "";
            cell[1] = model.ObjType ?? "";
            cell[2] = _item.PartNumber ?? "";
            cell[3] = _item.Name ?? "";
            cell[4] = _item.Specification ?? "";
            cell[5] = _item.Material ?? "";
            cell[6] = _item.Quantity.ToString() ?? "";
            cell[7] = model.FilePath ?? "";
            cell[8] = model.FileName ?? "";
            cell[9] = model.FileType ?? "";
            cell[10] = model.FileSize.ToString();
            cell[11] = model.CreateTime.ToString("yyyy-MM-dd");
            cell[12] = model.Creator ?? "";
        }
    }
}