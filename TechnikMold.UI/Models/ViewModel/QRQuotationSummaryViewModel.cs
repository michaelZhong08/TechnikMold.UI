using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.ViewModel
{
    public class QRQuotationSummaryViewModel
    {
        public IEnumerable<QRContent> Contents { get; set; }

        public IEnumerable<QRQuotation> Quotations { get; set; }

        public List<Supplier> Suppliers { get; set; }

        public IEnumerable<QRSupplier> QRSuppliers { get; set; }

        public int QRID { get; set; }

        public int QRState { get; set; }

        public QRQuotationSummaryViewModel(int QuotationRequestID, 
            int State,
            IEnumerable<QRContent> QRContents, 
            IEnumerable<QRQuotation> PRQuotations, 
            List<Supplier> Suppliers,
            IEnumerable<QRSupplier> QRSuppliers)
        {
            this.Contents = QRContents;
            this.Quotations = PRQuotations;
            this.Suppliers = Suppliers;
            this.QRSuppliers = QRSuppliers;
            this.QRID = QuotationRequestID;
            this.QRState = State;
        }
    }
}