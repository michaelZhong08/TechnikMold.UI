/*
 * Create By:lechun1
 * 
 * Description: data of purchase request quotation
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class QRQuotation
    {
        public int QRQuotationID { get; set; }
        public int QRContentID { get; set; }
        public int SupplierID { get; set; }
        public int QuotationRequestID { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime QuotationDate { get; set; }
        public bool Enabled { get; set; }
        public DateTime ShipDate { get; set; }
        public double UnitPriceWT { get; set; }
        public double TotalPriceWT { get; set; }
        public double TaxRate { get; set; }
    }
}
