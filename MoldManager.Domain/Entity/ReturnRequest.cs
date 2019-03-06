using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Status;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class ReturnRequest
    {
        public int ReturnRequestID { get; set; }
        public string ReturnRequestNumber { get; set; }
        public int PurchaseOrderID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int WarehouseUserID { get; set; }
        public int ApprovalUserID { get; set; }
        public int State { get; set; }
        public bool Enabled { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }

        public ReturnRequest()
        {
            ReturnRequestID=0;
            ReturnRequestNumber="";
            PurchaseOrderID=0;
            CreateDate=DateTime.Now;
            ApprovalDate=new DateTime(1900,1,1);
            ReturnDate=new DateTime(1900,1,1);
            WarehouseUserID=0;
            ApprovalUserID=0;
            State=(int)ReturnRequestStatus.新建;
            Enabled=true;
            SupplierID=0;
            SupplierName = "";
        }

        public ReturnRequest(PurchaseOrder PurchaseOrder)
        {
            ReturnRequestID = 0;
            ReturnRequestNumber = "";
            PurchaseOrderID = PurchaseOrder.PurchaseOrderID;
            CreateDate = DateTime.Now;
            ApprovalDate = new DateTime(1900, 1, 1);
            ReturnDate = new DateTime(1900, 1, 1);
            WarehouseUserID = 0;
            ApprovalUserID = 0;
            State = (int)ReturnRequestStatus.新建;
            Enabled = true;
            SupplierID = PurchaseOrder.SupplierID;
            SupplierName = PurchaseOrder.SupplierName;
        }
    }
}
