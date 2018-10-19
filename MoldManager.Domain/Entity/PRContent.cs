/*
 * Create By:lechun1
 * 
 * Description:data represent a content of purchase request
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class PRContent
    {
        public int PRContentID { get; set; }
        public int PartID { get; set; }
        public int TaskID { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string PartSpecification { get; set; }
        public int Quantity { get; set; }
        public int PartTypeID { get; set; }
        public int PurchaseRequestID { get; set; }
        public double UnitPrice { get; set; }
        public double SubTotal { get; set; }
        public string MaterialName { get; set; }
        public string Hardness { get; set; }
        public string JobNo { get; set; }
        public string BrandName { get; set; }
        public bool PurchaseDrawing { get; set; }
        public string Memo { get; set; }
        public double EstimatePrice { get; set; }
        public bool Enabled { get; set; }
        public int PurchaseItemID { get; set; }
        public DateTime RequireTime { get; set; }
        public string SupplierName { get; set; }
        public int WarehouseStockID { get; set; }
        public string MoldNumber { get; set; }
        public int CostCenterID { get; set; }
        public int PurchaseTypeID { get; set; }
        public string ERPPartID { get; set; }
    }
}
