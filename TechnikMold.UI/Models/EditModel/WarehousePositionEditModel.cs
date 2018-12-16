using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.EditModel
{
    public class WarehousePositionEditModel
    {
        public int StockID { get; set; }
        public string Name { get; set; }
        public string PartNum { get; set; }
        public string MaterialNumber { get; set; }
        public string Warehouse { get; set; }
        public string Position { get; set; }
        public int WarehouseID { get; set; }
        public int WarehousePositionID { get; set; }

        public WarehousePositionEditModel(WHStock StockItem,string name, string WarehouseName, string WarehousePositionName)
        {
            StockID = StockItem.ID;
            Name = name;
            PartNum = StockItem.PartNum;
            //MaterialNumber = StockItem.MaterialNumber;
            WarehouseID = StockItem.WarehouseID;
            WarehousePositionID = StockItem.WarehousePositionID;
            Warehouse = WarehouseName;
            Position = WarehousePositionName;
        }
    }
}