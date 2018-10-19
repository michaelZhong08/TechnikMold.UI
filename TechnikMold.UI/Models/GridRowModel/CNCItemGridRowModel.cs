using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class CNCItemGridRowModel
    {
        public string[] cell;
        public CNCItemGridRowModel(CNCItem Item, string RawSize)
        {
            cell = new string[9];
            cell[0] = Item.CNCItemID.ToString();
            cell[1] = Item.LabelName;
            cell[2] = Item.Ready.ToString() ;
            cell[3] = Item.Required.ToString();
            cell[4] = Item.Material;
            cell[5] = Item.SafetyHeight.ToString();
            cell[6] = RawSize;
            cell[7] = Item.LabelPrinted.ToString();
            cell[8] = Item.Status >= 20 ? true.ToString() : false.ToString();
        }
    }
}