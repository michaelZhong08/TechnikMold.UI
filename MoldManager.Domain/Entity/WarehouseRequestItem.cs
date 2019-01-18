using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class WarehouseRequestItem
    {
        public int WarehouseRequestItemID { get; set; }
        public int WarehouseRequestID { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string Specification { get; set; }
        public int PartID { get; set; }
        public int Quantity { get; set; }
        public int ReceivedQuantity { get; set; }
        public DateTime ReceiveDate { get; set; }
        public bool Received { get; set; }
        public int WarehouseStockID { get; set; }
        public double ShortQty { get; set; }


        public WarehouseRequestItem()
        {
            WarehouseRequestItemID = 0;
            WarehouseRequestID = 0;
            PartName = "";
            PartNumber = "";
            Specification = "";
            PartID = 0;
            Quantity = 0;
            ReceivedQuantity = 0;
            ReceiveDate = new DateTime(1900, 1, 1);
            Received = false;
            WarehouseStockID = 0;
            ShortQty = 0;
        }
    }
}
