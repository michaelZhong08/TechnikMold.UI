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
        public PartSearchGridRowModel(Part _part)
        {
            cell = new string[8];
            cell[0] = _part.PartID.ToString();
            cell[1] = _part.ShortName;
            cell[2] = _part.PartNumber;
            cell[3] = _part.Specification ?? "";
            cell[4] = _part.MaterialName ?? "";
            cell[5] = _part.BrandName ?? "";
            cell[6] = _part.Quantity.ToString();
            cell[7] = _part.Version;
        }
    }
}