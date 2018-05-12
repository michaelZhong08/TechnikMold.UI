using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class POContentGridRowModel
    {
        public string[] cell;
        public POContentGridRowModel(POContent POContent, string ETA, string PRNumber, int TypeID, string TypeName)
        {
            cell = new string[11];
            cell[0] = POContent.POContentID.ToString();
            cell[1] = POContent.PartName;
            cell[2] = POContent.PartNumber;
            cell[3] = POContent.PartSpecification;
            cell[4] = POContent.Quantity.ToString();
            cell[5] = POContent.UnitPrice.ToString();
            cell[6] = (POContent.Quantity * POContent.UnitPrice).ToString();
            cell[7] = PRNumber;
            cell[8] = POContent.RequireTime.ToString("yyyy-MM-dd");
            cell[9] = TypeID == 0 ? "" : TypeID.ToString();
            cell[10] = TypeName;
        }
    }
}