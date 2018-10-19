/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class WarehouseStock
    {
        public int WarehouseStockID { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public int Quantity { get; set; }
        public int SafeQuantity { get; set; }
        public int WarehouseID { get; set; }
        public bool Enabled { get; set; }
        public string MaterialNumber { get; set; }

        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int PurchaseItemID { get; set; }
        public int PurchaseType { get; set; }
        public int StockType { get; set; }
        public DateTime InStockTime { get; set; }
        public int InStockQty { get; set; }
        public int WarehouseUserID { get; set; }
        public string Material { get; set; }
        public string MoldNumber { get; set; }
        public int PlanQty { get; set; }
        public int WarehousePositionID { get; set; }
        public int OutStockQty { get; set; }

        public WarehouseStock()
        {
            WarehouseStockID = 0;
            Name = "";
            Specification = "";
            Quantity = 0;
            SafeQuantity = 0;
            WarehouseID = 0;
            Enabled = true;
            MaterialNumber = "";

            SupplierID = 0;
            SupplierName = "";
            PurchaseItemID = 0;
            PurchaseType = 0;
            StockType = 0;
            InStockTime = new DateTime(1900, 1, 1);
            InStockQty = 0;
            WarehouseUserID = 0;
            Material = "";
            MoldNumber = "";
            PlanQty = 0;
            WarehousePositionID = 0;
            OutStockQty = 0;

        }
    }
}
