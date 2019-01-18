using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class OutStockGridRowModel
    {
        public string[] cell;

        public OutStockGridRowModel(OutStockItem Item,OutStockForm Form, string ReceiveUser, string WarehouseUser,string _whRequestNum)
        {
            cell = new string[9];
            cell[0] = Item.OutStockItemID.ToString();
            cell[1] = _whRequestNum;
            cell[2] = Item.PartName;
            cell[3] = Item.PartNumber;
            cell[4] = Item.Specification;
            cell[5] = Item.Quantity.ToString();
            cell[6] = ReceiveUser; 
            cell[7] = Item.OutDate.ToString("yyyy-MM-dd HH:mm");
            cell[8] = WarehouseUser;
        }
    }
}