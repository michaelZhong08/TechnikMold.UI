using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class PRQuotationGridViewModel
    {
        public List<PRQuotationGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;
        public PRQuotationGridViewModel(int PurchaseRequestID, 
            IEnumerable<PRContent> PRContents, 
            IEnumerable<QRSupplier> PRSuppliers,
            IEnumerable<QRQuotation> PRQuotations)
        {
            rows = new List<PRQuotationGridRowModel>();

            foreach (PRContent _content in PRContents)
            {
                string[] SupplierName = new string[PRSuppliers.Count()];
                int[] SupplierID = new int[PRSuppliers.Count()];
                string[] Quotations = new string[PRQuotations.Count()];
                int[] QuotationID = new int[PRQuotations.Count()];
                int i = 0;
                foreach (QRSupplier _supplier in PRSuppliers)
                {
                    SupplierName[i] = _supplier.SupplierName;
                    SupplierID[i] = _supplier.SupplierID;
                    QRQuotation _quotation= PRQuotations.Where(p => p.SupplierID == _supplier.SupplierID).Where(p => p.QRContentID == _content.PRContentID).FirstOrDefault();
                    if (_quotation == null)
                    {
                        Quotations[i] = "-";
                    }
                    else
                    {
                        Quotations[i] = _quotation.UnitPrice.ToString();
                    }
                    PRQuotationGridRowModel _rowModel = new PRQuotationGridRowModel(_content, Quotations);
                    i = i + 1;
                }
            }
        }
    }
}