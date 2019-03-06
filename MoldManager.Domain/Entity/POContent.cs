/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class POContent
    {
        public int POContentID { get; set; }
        public int PRContentID { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string PartSpecification { get; set; }
        public int Quantity { get; set; }
        public int PurchaseOrderID { get; set; }
        public double UnitPrice { get; set; }
        public double SubTotal { get; set; }
        public string BrandName { get; set; }
        public string Memo { get; set; }
        public int ReceivedQty { get; set; }
        public int State { get; set; }
        public bool Enabled { get; set; }
        public int PurchaseItemID { get; set; }
        public DateTime RequireTime { get; set; }
        public string unit { get; set; }
        [NotMapped]
        public string PRNumber { get; set; }

        public POContent()
        {
            POContentID = 0;
            PRContentID = 0;
            PartName = "";
            PartNumber = "";
            PartSpecification = "";
            Quantity = 0;
            PurchaseOrderID = 0;
            UnitPrice = 0;
            SubTotal = 0;
            BrandName = "";
            Memo = "";
            ReceivedQty = 0;
            State = 0;
            Enabled = true;
            PurchaseItemID = 0;
            RequireTime = new DateTime(1900, 1, 1);
        }

        public POContent(PurchaseItem PurchaseItem, int ItemID)
        {
            POContentID = 0;
            PRContentID = 0;
            PartName = PurchaseItem.Name;
            PartNumber = PurchaseItem.PartNumber;
            PartSpecification = PurchaseItem.Specification;
            Quantity = PurchaseItem.Quantity;
            PurchaseOrderID = PurchaseItem.PurchaseOrderID;
            UnitPrice = PurchaseItem.UnitPriceWT;
            SubTotal = PurchaseItem.TotalPriceWT;
            BrandName ="";
            Memo = PurchaseItem.Memo;
            ReceivedQty = 0;
            State = 0;
            Enabled = true;
            PurchaseItemID = ItemID;
            RequireTime = PurchaseItem.PlanTime;
        }
    }
}
