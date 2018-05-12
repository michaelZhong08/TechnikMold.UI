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

        public OutStockGridRowModel(OutStockItem Item,OutStockForm Form, string ReceiveUser, string WarehouseUser)
        {
            cell = new string[8];
            cell[0] = Item.OutStockItemID.ToString();
            cell[1] = Item.PartName;
            cell[2] = Item.PartNumber;
            cell[3] = Item.Specification;
            cell[4] = Item.Quantity.ToString();
            cell[5] = ReceiveUser; 
            cell[6] = Item.OutDate.ToString("yyyy-MM-dd HH:mm");
            cell[7] = WarehouseUser;
        }
    }
}