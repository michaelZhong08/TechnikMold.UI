using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class QRContent
    {
        public int QRContentID { get; set; }
        public int PRContentID { get; set; }
        public int PartID { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string PartSpecification { get; set; }
        public int Quantity { get; set; }
        public int PurchaseRequestID { get; set; }
        public int QuotationRequestID { get; set; }
        public string MaterialName { get; set; }
        public string Hardness { get; set; }
        public string BrandName { get; set; }
        public bool PurchaseDrawing { get; set; }
        public string Memo { get; set; }
        public bool Enabled { get; set; }
        public int PurchaseItemID { get; set; }
        public DateTime RequireDate { get; set; }
        public int SupplierID { get; set; }
    }
}
