/*
 * Create By:lechun1
 * 
 * Description:data represent supplier in purchase request
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class QRSupplier
    {
        public int QRSupplierID { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int QuotationRequestID { get; set; }
        public DateTime RequestDate { get; set; }
        public bool QuotationState { get; set; }
        public DateTime QuotationDate { get; set; }
        public DateTime ValidDate { get; set; }
        public double TaxRate { get; set; }
        public bool TaxInclude { get; set; }
        public int ContactID { get; set; }
        public bool Enabled { get; set; }
    }
}
