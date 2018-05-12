using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Status;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class PurchaseItem
    {
        public int PurchaseItemID { get; set; }
        public int PartID { get; set; }
        public int TaskID { get; set; }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string Specification { get; set; }
        public string Material { get; set; }
        public int Quantity { get; set; }
        public string SupplierName { get; set; }
        public int SupplierID { get; set; }
        public DateTime PlanTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public int State { get; set; }
        public int PurchaseRequestID { get; set; }
        public int QuotationRequestID { get; set; }
        public int PurchaseOrderID { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public string Memo { get; set; }
        public DateTime RequireTime { get; set; }
        public int PurchaseUserID { get; set; }
        public int InStockQty { get; set; }
        public int OutStockQty { get; set; }
        public int RequestUserID { get; set; }
        public int PurchaseType { get; set; }
        public int WarehouseStockID { get; set; }
        public string MoldNumber { get; set; }
        public double UnitPriceWT { get; set; }
        public double TotalPriceWT { get; set; }
        public double TaxRate { get; set; }
        public int CostCenterID { get; set; }
        public int ProcedureType { get; set; }


        public PurchaseItem()
        {
            PurchaseItemID = 0;
            PartID = 0;
            TaskID = 0;
            Name="";
            PartNumber = "";
            Specification = "";
            Material = "";
            Quantity = 0;
            SupplierName = "";
            SupplierID = 0;
            PlanTime = new DateTime(1900, 1, 1);
            CreateTime = DateTime.Now;
            DeliveryTime = new DateTime(1900, 1, 1);
            State = (int)PurchaseItemStatus.未提交;
            PurchaseRequestID = 0;
            QuotationRequestID = 0;
            PurchaseOrderID = 0;
            UnitPrice = 0;
            TotalPrice = 0;
            Memo = "";
            RequireTime = new DateTime(1900, 1, 1);
            PurchaseUserID =0;
            InStockQty =0;
            OutStockQty = 0;
            RequestUserID = 0;
            PurchaseType = 0;
            WarehouseStockID = 0;
            MoldNumber = "";
            UnitPriceWT = 0;
            TotalPriceWT = 0;
            TaxRate = 0;
            CostCenterID = 0;
            ProcedureType = 0;
        }

        public PurchaseItem(PRContent PRContent)
        {
            PurchaseItemID = PRContent.PurchaseItemID;
            PartID = PRContent.PartID;
            TaskID = PRContent.TaskID;
            Name = PRContent.PartName;
            PartNumber = PRContent.PartNumber;
            Specification = PRContent.PartSpecification;
            Material = PRContent.MaterialName;
            Quantity = PRContent.Quantity;
            SupplierName = PRContent.SupplierName;
            SupplierID = 0;
            PlanTime = new DateTime(1900, 1, 1);
            CreateTime = DateTime.Now;
            DeliveryTime = new DateTime(1900, 1, 1);
            State = (int)PurchaseItemStatus.未提交;
            PurchaseRequestID = PRContent.PurchaseRequestID;
            QuotationRequestID = 0;
            PurchaseOrderID = 0;
            UnitPrice = 0;
            TotalPrice = 0;
            Memo = PRContent.Memo;
            RequireTime = PRContent.RequireTime == new DateTime() ? new DateTime(1900, 1, 1) : PRContent.RequireTime;
            PurchaseUserID = 0;
            InStockQty = 0;
            OutStockQty = 0;
            RequestUserID = 0;
            PurchaseType = 0;
            WarehouseStockID = PRContent.WarehouseStockID;
            MoldNumber = "";
            UnitPriceWT = 0;
            TotalPriceWT = 0;
            TaxRate = 0;
            CostCenterID = 0;
            ProcedureType = 0;
        }

        public PurchaseItem(QRContent QRContent)
        {
            PurchaseItemID = 0;
            PartID = QRContent.PartID;
            TaskID = 0;
            Name = QRContent.PartName;
            PartNumber = QRContent.PartNumber;
            Specification = QRContent.PartSpecification;
            Material = QRContent.MaterialName;
            Quantity = QRContent.Quantity;
            SupplierName = "";
            SupplierID = 0;
            PlanTime = new DateTime(1900, 1, 1);
            CreateTime = DateTime.Now;
            DeliveryTime = new DateTime(1900, 1, 1);
            State = (int)PurchaseItemStatus.待询价;
            PurchaseRequestID = 0;
            QuotationRequestID = QRContent.QuotationRequestID;
            PurchaseOrderID = 0;
            UnitPrice = 0;
            TotalPrice = 0;
            Memo = QRContent.Memo==null?"":QRContent.Memo;
            RequireTime = new DateTime(1900, 1, 1);
            PurchaseUserID = 0;
            InStockQty = 0;
            OutStockQty = 0;
            RequestUserID = 0;
            PurchaseType = 0;
            MoldNumber = "";
            UnitPriceWT = 0;
            TotalPriceWT = 0;
            TaxRate = 0;
            CostCenterID = 0;
            ProcedureType = 0;
        }
    }
}
