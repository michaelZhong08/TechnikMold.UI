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
    public class PurchaseOrder
    {
        public int PurchaseOrderID { get; set; }
        public int PurchaseRequestID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int State { get; set; }
        public int UserID { get; set; }
        public int Responsible { get; set; }
        public int Approval { get; set; }
        public int ProjectID { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime FinishDate { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPriceWT { get; set; }
        public int SupplierID { get; set; }
        public string Memo { get; set; }
        public DateTime DueDate { get; set; }
        public int QuotationRequestID { get; set; }
        public int TaxRate { get; set; }
        public string Currency { get; set; }
        public int PurchaseType { get; set; }
        public string SupplierName { get; set; }
        public DateTime CreateDate { get; set; }

        public PurchaseOrder()
        {
            PurchaseOrderID = 0;
            PurchaseRequestID = 0;
            PurchaseOrderNumber = "";
            State = 1;
            UserID = 0;
            Responsible = 0;
            Approval = 0;
            ProjectID = 0;
            ReleaseDate = new DateTime(1900, 1, 1);
            FinishDate = new DateTime(1900, 1, 1);
            TotalPrice = 0;
            SupplierID = 0;
            Memo = "";
            DueDate = new DateTime(1900, 1, 1);
            QuotationRequestID = 0;
            TaxRate = 0;
            Currency = "";
            PurchaseType = 0;
            SupplierName = "";
            CreateDate = DateTime.Now;

        }

    }
}
