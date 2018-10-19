using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class ReturnItem
    {
        public int ReturnItemID { get; set; }
        public string Name { get; set; }
        public string MaterialNumber { get; set; }
        public string Specification { get; set; }
        public int Quantity { get; set; }
        public int ReturnRequestID { get; set; }
        public int WarehouseItemID { get; set; }
        public int PurchaseItemID { get; set; }
        public int State { get; set; }
        public string Memo { get; set; }
        public bool Enabled { get; set; }


        public ReturnItem()
        {
            ReturnItemID=0;
            Name="";
            MaterialNumber="";
            Specification="";
            Quantity=0;
            ReturnRequestID=0;
            WarehouseItemID=0;
            PurchaseItemID=0;
            State=0;
            Memo="";
            Enabled=true;
        }

        public ReturnItem(PurchaseItem Item, int WarehouseStockID){
            ReturnItemID=0;
            Name=Item.Name;
            MaterialNumber=Item.PartNumber;
            Specification=Item.Specification;
            Quantity=Item.InStockQty;
            ReturnRequestID=0;
            WarehouseItemID=WarehouseStockID;
            PurchaseItemID=Item.PurchaseItemID;
            State=0;
            Memo="";
            Enabled=true;
        }
    }
}
