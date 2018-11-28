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

        public List<SupplierGroup> SupplierGroups { get; set; }

        public IEnumerable<QRSupplier> QRSuppliers { get; set; }

        public int QRID { get; set; }

        public int QRState { get; set; }

        public QRQuotationSummaryViewModel(int QuotationRequestID, 
            int State,
            IEnumerable<QRContent> QRContents, 
            IEnumerable<QRQuotation> PRQuotations, 
            List<SupplierGroup> SupplierGroups,
            IEnumerable<QRSupplier> QRSuppliers)
        {
            this.Contents = QRContents;
            this.Quotations = PRQuotations;
            this.SupplierGroups = SupplierGroups;
            this.QRSuppliers = QRSuppliers;
            this.QRID = QuotationRequestID;
            this.QRState = State;
        }
    }
}