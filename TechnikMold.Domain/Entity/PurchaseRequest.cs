/*
 * Create By:lechun1
 * 
 * Description: data represent a purchase request
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class PurchaseRequest
    {
        public int PurchaseRequestID { get; set; }
        public string PurchaseRequestNumber { get; set; }
        public int State { get; set; }
        public int UserID { get; set; }
        public int Responsible { get; set; }
        public int Approval { get; set; }
        public int ProjectID { get; set; }
        public int SupplierID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime AcceptDate { get; set; }
        public double TotalPrice { get; set; }
        public string Memo { get; set; }
        public bool Enabled { get; set; }
        public DateTime DueDate { get; set; }

        public int PurchaseType { get; set; }
        public int DepartmentID { get; set; }
        public int SubmitUserID { get; set; }
        public int ReviewUserID { get; set; }


        public PurchaseRequest()
        {
            PurchaseRequestID = 0;
            PurchaseRequestNumber = "";
            State = -100;
            UserID = 0;
            Responsible = 0;
            Approval = 0;
            ProjectID = 0;
            SupplierID = 0;
            CreateDate = new DateTime(1900, 1, 1);
            ApprovalDate = new DateTime(1900, 1, 1);
            AcceptDate = new DateTime(1900, 1, 1);
            TotalPrice = 0;
            Memo = "";
            Enabled = true;
            DueDate = new DateTime(1900, 1, 1);
            PurchaseType = 0;
            DepartmentID = 0;
            SubmitUserID = 0;
            ReviewUserID = 0;

        }
    }
}
